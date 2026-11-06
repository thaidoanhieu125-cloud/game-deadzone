
using UnityEngine;

public class Bullet_Map3 : MonoBehaviour
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
        ZombieHealth_Map3 zombie = collision.GetComponent<ZombieHealth_Map3>();
        BossHealth_Map3 boss = collision.GetComponent<BossHealth_Map3>();

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
