using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager_Map5 : MonoBehaviour
{
    // Cập nhật tham chiếu tới các class _Map5
    public GoldManager_Map5 goldManager;
    public PlayerHealth_Map5 playerHealth;
    public GunShoot_Map5 gunShoot;
    public PlayerController_Map5 playerControler;

    public Image[] healthBoxes;
    public Image[] damageBoxes;
    public Image[] speedBoxes;
    public Image[] ammoBoxes;

    private int[] prices = { 100, 200, 350, 500, 700 };

    public TMP_Text textHealth;
    public TMP_Text textDamage;
    public TMP_Text textSpeed;
    public TMP_Text textAmmo;

    public int healthLevel = 1;
    public int damageLevel = 1;
    public int speedLevel = 1;
    public int ammoLevel = 1;

    public int maxHealth = 20;
    public int damage = 5;
    public float speed = 0.5f;
    public int maxAmmo = 10;

    // --- THÊM PHẦN ÂM THANH ---
    public AudioSource shopAudioSource; // Kéo AudioSource vào đây
    public AudioClip clickClip;         // Gán file "Click"
    public AudioClip buyClip;           // Gán file "Mua"
    // --------------------------
    void Start()
    {
        UpdateAllBoxes();
    }

    bool SpendGold(int cost)
    {
        //if (goldManager.gold < cost)
        //    return false;
        if (goldManager.gold < cost)
        {
            // Tình huống KHÔNG ĐỦ TIỀN: Dùng tiếng Click nhưng chỉnh Pitch thấp xuống
            if (shopAudioSource != null && clickClip != null)
            {
                shopAudioSource.pitch = 0.5f; // Chỉnh âm trầm
                shopAudioSource.PlayOneShot(clickClip);
                shopAudioSource.pitch = 1.0f; // Reset lại âm bình thường
            }
            return false;
        }
        goldManager.gold -= cost;
        goldManager.UpdateUI();
        if (shopAudioSource != null && buyClip != null)
        {
            shopAudioSource.PlayOneShot(buyClip);
        }
        return true;
    }

    void UpdateBox(Image[] boxes, int level)
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (i < level)
                boxes[i].color = Color.red;
            else
                boxes[i].color = Color.white;
        }
    }

    void UpdateAllBoxes()
    {
        UpdateBox(healthBoxes, healthLevel);
        UpdateBox(damageBoxes, damageLevel);
        UpdateBox(speedBoxes, speedLevel);
        UpdateBox(ammoBoxes, ammoBoxes.Length > 0 ? ammoLevel : 0); // Đảm bảo an toàn
    }

    public void BuyHealth()
    {
        if (healthLevel >= 5) return;

        int cost = prices[healthLevel];
        if (!SpendGold(cost)) return;

        healthLevel++;
        textHealth.text = "Giá: " + cost.ToString();

        playerHealth.maxHealth += maxHealth;
        playerHealth.currentHealth += maxHealth;
        playerHealth.UpdateHealthUI();

        UpdateAllBoxes();
    }

    public void BuyDamage()
    {
        if (damageLevel >= 5) return;

        int cost = prices[damageLevel];
        if (!SpendGold(cost)) return;

        damageLevel++;
        textDamage.text = "Giá: " + cost.ToString();

        gunShoot.damage += damage;
        UpdateAllBoxes();
    }

    public void BuySpeed()
    {
        if (speedLevel >= 4) return;

        int cost = prices[speedLevel];
        if (!SpendGold(cost)) return;

        speedLevel++;
        textSpeed.text = "Giá: " + cost.ToString();

        playerControler.speed += speed;
        UpdateAllBoxes();
    }

    public void BuyAmmo()
    {
        if (ammoLevel >= 4) return;

        int cost = prices[ammoLevel];
        if (!SpendGold(cost)) return;

        ammoLevel++;
        textAmmo.text = "Giá: " + cost.ToString();

        gunShoot.maxAmmo += maxAmmo;
        gunShoot.currentAmmo += maxAmmo;
        gunShoot.UpdateAmmoUI();
        UpdateAllBoxes();
    }
    // Đừng quên thêm hàm này cho các Button trong shop để có tiếng click chuẩn:
    public void PlayClickSound()
    {
        if (shopAudioSource != null && clickClip != null)
        {
            shopAudioSource.pitch = 1.0f;
            shopAudioSource.PlayOneShot(clickClip);
        }
    }
}