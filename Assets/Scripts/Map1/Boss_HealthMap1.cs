using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthMap1 : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Animator anima;
    private bool isDead = false;
    public TMP_Text healthText;
    public Image healthFill;
    public GameObject goldPrefab;
    public GameObject ammoPrefab;
    public GameObject healthPrefab;
    public int zombieDieAddGold = 200;
    public int zombieDieAddHealth = 30;
    private bool rageMode = false;
    void Start()
    {
        UpdateHealthUI();
        currentHealth = maxHealth;
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
            print("giảm damage");
            damage = Mathf.RoundToInt(damage * 0.75f);
            print(" damage" + damage);
        }
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();
        print("test");


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

        //rơi vàng
        GoldManagerMap1 gold = FindAnyObjectByType<GoldManagerMap1>();
        if (gold != null)
        {
            gold.AddGold(zombieDieAddGold);
        }

        // rơi đạn
        GunShootMap1 gun = FindFirstObjectByType<GunShootMap1>();
        if (gun != null)
        {
            // ammo.GetComponent<SupportPack>().ammoAmount = gun.currentAmmo = gun.maxAmmo;
            gun.currentAmmo = gun.maxAmmo;
            gun.UpdateAmmoUI();
        }

        // rơi máu
        PlayerHealthMap1 playerHealth = FindAnyObjectByType<PlayerHealthMap1>();
        if (playerHealth != null)
        {
            playerHealth.addHeal(zombieDieAddHealth);
        }

        if (isDead) return;

        isDead = true;
        anima.SetTrigger("die");
        
        StartCoroutine(MissionComplete());
        // Destroy(gameObject, 0.5f); // chờ animation chết
    }

    public void UpdateHealthUI()
    {
        healthFill.fillAmount = (float)currentHealth / maxHealth;
        healthText.text = "HP: " + maxHealth + " / " + currentHealth;
    }
    IEnumerator MissionComplete()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        FullUI_Map1 gameOver = FindFirstObjectByType<FullUI_Map1>();
        ZombieSpawnerMap1 spawner = FindFirstObjectByType<ZombieSpawnerMap1>();

        if (gameOver != null && spawner != null)
        {
            gameOver.showMissionComplete(spawner.zombiesKilled,spawner.totalZombieToSpawn);

        }
    }
}
