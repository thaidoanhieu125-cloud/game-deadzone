using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 3f;
    
    [HideInInspector]
    public int damage = 1;

    public Rigidbody2D rb;
    public GameObject wallHitEffect;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        if (collision.name.Contains("Confiner")) return;
        ZombieHealth zombie = collision.GetComponent<ZombieHealth>();
        BossHealth boss = collision.GetComponent<BossHealth>();

        if (zombie != null)
        {
            zombie.TakeDamage(damage);
        }

        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        // tóe lửa khi trúng tường
        if (collision.CompareTag("Wall") ||
            collision.CompareTag("Lab")||
            collision.CompareTag("Props"))
        {
            GameObject effect = Instantiate(
                wallHitEffect,
                transform.position,
                Quaternion.identity
            );
            // print("nổ");
            Destroy(effect, 0.1f);
        }

        Destroy(gameObject);
        // Debug.Log("Hit: " + collision.name);
    }
}