using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss_Health_Map5 : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Animator anima;
    private bool isDead = false;
    public TMP_Text healthText;
    public Image healthFill;

    // Các vật phẩm rơi ra
    public GameObject goldPrefab;
    public GameObject ammoPrefab;
    public GameObject healthPrefab;

    public int zombieDieAddGold = 200;
    public int zombieDieAddHealth = 30;
    private bool rageMode = false;

    void Start()
    {
        currentHealth = maxHealth; // Đặt máu ngay khi bắt đầu
        UpdateHealthUI();
        anima = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        // Kích hoạt rageMode khi còn 50% máu
        if (!rageMode && currentHealth <= maxHealth / 2)
        {
            rageMode = true;
            anima.SetTrigger("rageMode");
            // Giảm sát thương nhận vào
            damage = Mathf.RoundToInt(damage * 0.75f);
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

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
        if (isDead) return;
        isDead = true;

        // Rơi vàng
        GoldManager gold = FindAnyObjectByType<GoldManager>();
        if (gold != null)
        {
            gold.AddGold(zombieDieAddGold);
        }

        // Rơi đạn (Cập nhật theo script GunShoot của Map 5)
        GunShoot gun = FindFirstObjectByType<GunShoot>();
        if (gun != null)
        {
            gun.currentAmmo = gun.maxAmmo;
            gun.UpdateAmmoUI();
        }

        // Rơi máu
        PlayerHealth_Map5 playerHealth = FindAnyObjectByType<PlayerHealth_Map5>();
        if (playerHealth != null)
        {
            playerHealth.addHeal(zombieDieAddHealth);
        }
        CancelInvoke("PlayCallSound");
        anima.SetTrigger("die");
        
        StartCoroutine(MissionComplete());
    }

    public void UpdateHealthUI()
    {
        if (healthFill != null)
            healthFill.fillAmount = (float)currentHealth / maxHealth;

        if (healthText != null)
            healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }

    //IEnumerator MissionComplete()
    //{
    //    yield return new WaitForSecondsRealtime(1.5f);

    //    FullUI_Map5 gameOver = FindFirstObjectByType<FullUI_Map5>();
    //    ZombieSpawner_Map5 spawner = FindFirstObjectByType<ZombieSpawner_Map5>();

    //    if (gameOver != null && spawner != null)
    //    {
    //        gameOver.showMissionComplete(spawner.zombiesKilled, spawner.totalZombieToSpawn);
    //    }
    //}
    IEnumerator MissionComplete()
    {
        // Đợi 1.5 giây để animation "die" chạy xong
        yield return new WaitForSeconds(1.5f);

        FullUI_Map5 uiManager = FindFirstObjectByType<FullUI_Map5>();
        ZombieSpawner_Map5 spawner = FindFirstObjectByType<ZombieSpawner_Map5>();

        if (uiManager != null)
        { 
            // Truyền dữ liệu zombie đã giết từ spawner
            int killed = (spawner != null) ? spawner.zombiesKilled : 0;
            int total = (spawner != null) ? spawner.totalZombieToSpawn : 0;

            uiManager.showMissionComplete(killed, total);
        }
    }
}