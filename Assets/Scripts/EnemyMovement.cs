using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Animator anima;
    [SerializeField] private float speed;
    [SerializeField] protected bool isBoss;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField, Min(0.4f)] private float navigationCellSize = 0.5f;

    [Header("Audio")]
    [FormerlySerializedAs("zombieAudioSource")]
    [FormerlySerializedAs("bossAudioSource")]
    [FormerlySerializedAs("audioSourceRun")]
    [SerializeField] private AudioSource vocalSource;
    [FormerlySerializedAs("growlSound")]
    [FormerlySerializedAs("bossGrowlSound")]
    [FormerlySerializedAs("callClip")]
    [SerializeField] private AudioClip growlClip;
    [SerializeField] protected float minGrowlDelay = 5f;
    [SerializeField] protected float maxGrowlDelay = 10f;
    [SerializeField] protected float growlRange;
    [SerializeField, Range(0f, 1f)] protected float growlVolume = 1f;
    [FormerlySerializedAs("damageClip")]
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip footstepClip;

    private static readonly int Moving = Animator.StringToHash("isMoving");
    private static readonly int Attack = Animator.StringToHash("attack");
    private static readonly int BossAttack = Animator.StringToHash("attackBoss");

    private Rigidbody2D body;
    private Transform player;
    private Collider2D playerCollider;
    private ChaseNavigation2D navigation;
    private int facingDirection = 1;
    private int playerContacts;
    private bool canAttack;
    private bool isAttacking;
    private float attackTimer;
    private float growlTimer;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        navigation = new ChaseNavigation2D(body, obstacleLayer, navigationCellSize);
        if (vocalSource == null)
            vocalSource = GetComponent<AudioSource>();
        if (footstepSource != null)
            footstepSource.clip = footstepClip;

        if (growlRange <= 0f)
            growlTimer = Random.Range(minGrowlDelay, maxGrowlDelay);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
            playerCollider = player.GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;
        canAttack = false;
        if (player != null && player.gameObject.activeInHierarchy)
        {
            Vector2 destination = playerCollider != null && playerCollider.enabled
                ? playerCollider.bounds.center
                : player.position;
            canAttack = playerContacts > 0 && navigation.HasLineOfSight(body.position, destination);
            if (!canAttack)
                direction = navigation.GetDirection(body.position, destination);

            Face(destination.x - body.position.x);
        }

        body.linearVelocity = direction * speed;
        bool moving = body.linearVelocity != Vector2.zero;
        anima.SetBool(Moving, moving);

        if (footstepSource != null && footstepClip != null)
        {
            if (moving && !footstepSource.isPlaying)
                footstepSource.Play();
            else if (!moving && footstepSource.isPlaying)
                footstepSource.Stop();
        }
    }

    private void Update()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
            return;

        UpdateGrowl();
        if (!canAttack)
            return;

        if (attackTimer <= 0f && !isAttacking)
        {
            isAttacking = true;
            if (vocalSource != null && attackClip != null)
                vocalSource.PlayOneShot(attackClip);
            anima.SetTrigger(isBoss ? BossAttack : Attack);
            attackTimer = attackCooldown;
        }
        attackTimer -= Time.deltaTime;
    }

    private void UpdateGrowl()
    {
        if (vocalSource == null || growlClip == null)
            return;
        if (growlRange > 0f && (canAttack || ((Vector2)player.position - body.position).sqrMagnitude >= growlRange * growlRange))
            return;

        growlTimer -= Time.deltaTime;
        if (growlTimer > 0f)
            return;

        if (growlRange > 0f || !vocalSource.isPlaying)
            vocalSource.PlayOneShot(growlClip, growlVolume);
        growlTimer = Random.Range(minGrowlDelay, maxGrowlDelay);
    }

    private void Face(float horizontal)
    {
        if (Mathf.Abs(horizontal) < 0.001f || horizontal * facingDirection > 0f)
            return;

        facingDirection = -facingDirection;
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerContacts++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerContacts = Mathf.Max(0, playerContacts - 1);
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
}
