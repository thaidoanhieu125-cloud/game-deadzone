using UnityEngine;

public class Bullet_5 : MonoBehaviour
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
        // Đảm bảo velocity được áp dụng đúng hướng
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        if (collision.name.Contains("Confiner")) return;

        // Cập nhật tham chiếu tới các class _Map5
        ZombieHealth_Map5 zombie = collision.GetComponent<ZombieHealth_Map5>();
        Boss_Health_Map5 boss = collision.GetComponent<Boss_Health_Map5>();

        if (zombie != null)
        {
            zombie.TakeDamage(damage);
        }

        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        // Tạo hiệu ứng khi trúng vật cản
        if (collision.CompareTag("Wall") ||
            collision.CompareTag("Lab") ||
            collision.CompareTag("Props"))
        {
            if (wallHitEffect != null)
            {
                GameObject effect = Instantiate(
                    wallHitEffect,
                    transform.position,
                    Quaternion.identity
                );
                Destroy(effect, 0.1f);
            }
        }

        Destroy(gameObject);
    }
}