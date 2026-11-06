using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Animator anima;
    private bool isDead = false;

    public GameObject goldPrefab;
    public GameObject ammoPrefab;
    public GameObject healthPrefab;
    public int zombieDieAddGold = 10;
    public int zombieDieAddAmmo = 10;
    public int zombieDieAddHealth = 30;
    public AudioSource audioSourceHealth;
    public AudioClip zombieHitClip;
    public AudioClip zombieDieClip;
    void Start()
    {
        currentHealth = maxHealth;
        anima = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        audioSourceHealth.PlayOneShot(zombieHitClip);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            anima.SetTrigger("takeDamage");
        }
    }

    void Die()
    {
        Vector3 pos = transform.position;

        // 100% rơi vàng
        GameObject gold = Instantiate(
            goldPrefab,
            pos + new Vector3(-1.3f, 0, 0),
            Quaternion.identity
        );
        gold.GetComponent<SupportPack>().goldAmount = zombieDieAddGold;

        // 50% rơi đạn
        if (Random.value <= 0.5f)
        {
            GameObject ammo = Instantiate(
                ammoPrefab,
               pos + new Vector3(1.3f, 0, 0),
                Quaternion.identity
            );
            ammo.GetComponent<SupportPack>().ammoAmount = zombieDieAddAmmo;

        }

        // 10% rơi máu
        if (Random.value <= 0.1f)
        {
            GameObject health = Instantiate(
                healthPrefab,
                 pos + new Vector3(0, 1.3f, 0),
                Quaternion.identity
            );
            health.GetComponent<SupportPack>().healtAmount = zombieDieAddHealth;
        }

        if (isDead) return;

        isDead = true;
        audioSourceHealth.PlayOneShot(zombieDieClip);
        anima.SetTrigger("die");
        ZombieSpawner spawner =
            FindFirstObjectByType<ZombieSpawner>();

        if (spawner != null)
        {
            spawner.zombieAlive--;
            spawner.zombiesKilled++;
            spawner.ZombieAliveText();
        }

        Destroy(gameObject, 0.5f); // chờ animation chết
    }

}