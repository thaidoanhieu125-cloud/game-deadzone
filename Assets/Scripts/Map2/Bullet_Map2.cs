using UnityEngine;

public class Bullet_Map2 : MonoBehaviour
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
        ZombieHealth_Map2 zombie = collision.GetComponent<ZombieHealth_Map2>();
        BossHealth_Map2 boss = collision.GetComponent<BossHealth_Map2>();

        if (zombie != null)
        {
            zombie.TakeDamage(damage);
        }

        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        // tóe lửa khi trúng tường
        if (collision.CompareTag("Wall"))
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
