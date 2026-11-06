using UnityEngine;

public class ZombieHealth_Map5 : MonoBehaviour
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

    // --- THÊM PHẦN ÂM THANH ---
    public AudioSource audioSource; // Gán AudioSource của Zombie vào đây
    public AudioClip hitSound;      // Gán file: zombiebiban
    public AudioClip dieSound;      // Gán file: zombieChet
    public AudioClip attackSound; // Gán: ZombieAttack
    public AudioClip runSound;    // Gán: ZombieRun
    // --------------------------
    void Start()
    {
        currentHealth = maxHealth;
        anima = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        // --- PHÁT TIẾNG BỊ BẮN ---
        if (hitSound != null && audioSource != null)
            audioSource.PlayOneShot(hitSound);
        // -------------------------

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
        if (isDead) return; // Kiểm tra an toàn
        isDead = true;
        // --- PHÁT TIẾNG CHẾT ---
        if (dieSound != null && audioSource != null)
            audioSource.PlayOneShot(dieSound);
        // -----------------------
        Vector3 pos = transform.position;

        // 100% rơi vàng
        GameObject gold = Instantiate(goldPrefab, pos + new Vector3(-1.3f, 0, 0), Quaternion.identity);
        gold.GetComponent<SupportPack_Map5>().goldAmount = zombieDieAddGold;

        // 50% rơi đạn
        if (Random.value <= 0.5f)
        {
            GameObject ammo = Instantiate(ammoPrefab, pos + new Vector3(1.3f, 0, 0), Quaternion.identity);
            ammo.GetComponent<SupportPack_Map5>().ammoAmount = zombieDieAddAmmo;
        }

        // 10% rơi máu
        if (Random.value <= 0.1f)
        {
            GameObject health = Instantiate(healthPrefab, pos + new Vector3(0, 1.3f, 0), Quaternion.identity);
            health.GetComponent<SupportPack_Map5>().healtAmount = zombieDieAddHealth;
        }

        anima.SetTrigger("die");

        // Cập nhật tham chiếu tới ZombieSpawner_Map5
        ZombieSpawner_Map5 spawner = FindFirstObjectByType<ZombieSpawner_Map5>();

        if (spawner != null)
        {
            spawner.zombieAlive--;
            spawner.zombiesKilled++;
            spawner.ZombieAliveText();
        }
        
        Destroy(gameObject, 0.5f); // chờ animation chết
    }
    public void PlayAttack()
    {
        if (attackSound != null && audioSource != null)
            audioSource.PlayOneShot(attackSound);
    }

    public void PlayRun()
    {
        // Kiểm tra để tránh phát âm thanh quá dồn dập
        if (!audioSource.isPlaying && runSound != null)
        {
            audioSource.PlayOneShot(runSound);
        }
    }
}