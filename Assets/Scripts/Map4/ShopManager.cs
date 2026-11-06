using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GoldManager goldManager;
    public PlayerHealth playerHealth;
    public GunShoot gunShoot;
    public PlayerControler playerControler;

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
    public AudioSource audioSource;
    public AudioClip shopClip;

    void Start()
    {
        UpdateAllBoxes();
    }

    bool SpendGold(int cost)
    {
        if (goldManager.gold < cost)
            return false;

        goldManager.gold -= cost;
        goldManager.UpdateUI();

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
        UpdateBox(ammoBoxes, ammoLevel);
         UpdatePriceText();
    }
    void UpdatePriceText()
    {
        textHealth.text =
            healthLevel < prices.Length
            ? "Giá: " + prices[healthLevel]
            : "MAX";

        textDamage.text =
            damageLevel < prices.Length
            ? "Giá: " + prices[damageLevel]
            : "MAX";

        textSpeed.text =
            speedLevel < 4
            ? "Giá: " + prices[speedLevel]
            : "MAX";

        textAmmo.text =
            ammoLevel < 4
            ? "Giá: " + prices[ammoLevel]
            : "MAX";
    }
    public void BuyHealth()
    {
        if (healthLevel >= 5)
            return;

        int cost = prices[healthLevel];

        if (!SpendGold(cost))
            return;

        healthLevel++;
        audioSource.PlayOneShot(shopClip);
        playerHealth.maxHealth += maxHealth;
        playerHealth.currentHealth += maxHealth;
        playerHealth.UpdateHealthUI();

        UpdateAllBoxes();
    }

    public void BuyDamage()
    {
        if (damageLevel >= 5)
            return;

        int cost = prices[damageLevel];

        if (!SpendGold(cost))
            return;

        damageLevel++;
        audioSource.PlayOneShot(shopClip);
        gunShoot.damage += damage;

        UpdateAllBoxes();
    }

    public void BuySpeed()
    {
        if (speedLevel >= 4)
            return;

        int cost = prices[speedLevel];

        if (!SpendGold(cost))
            return;

        speedLevel++;
        audioSource.PlayOneShot(shopClip);
        playerControler.speed += speed;

        UpdateAllBoxes();
    }

    public void BuyAmmo()
    {
        if (ammoLevel >= 4)
            return;

        int cost = prices[ammoLevel];

        if (!SpendGold(cost))
            return;

        ammoLevel++;
        audioSource.PlayOneShot(shopClip);
        gunShoot.maxAmmo += maxAmmo;
        gunShoot.currentAmmo += maxAmmo;
        gunShoot.UpdateAmmoUI();
        UpdateAllBoxes();
    }
}