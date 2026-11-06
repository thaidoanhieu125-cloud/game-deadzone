using UnityEngine;

public class BulletMap1 : MonoBehaviour
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
        ZombieHealthMap1 zombie = collision.GetComponent<ZombieHealthMap1>();
        BossHealthMap1 boss = collision.GetComponent<BossHealthMap1>();

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
            collision.CompareTag("Container") ||
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