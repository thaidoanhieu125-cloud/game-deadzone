using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public TMP_Text healthText;
    public Animator anima;
    private bool isDead = false;
    public Image healthFill;
    public AudioSource audioSourcePlayerSFX;
    public AudioClip addHealthClip;
    public AudioClip playerHitClip;
    public AudioClip dieClip; 
    void Start()
    {
        //hien thi mau
        UpdateHealthUI();
        

    }
    public void ChangeHealth(int amount)
    {
        if (isDead) return;

        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //hien thi mau
        UpdateHealthUI();
        anima.SetTrigger("hit");
        audioSourcePlayerSFX.PlayOneShot(playerHitClip);
        // print("hit");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public bool addHeal(int amount)
    {


        if (currentHealth >= maxHealth)
        {
            // print("máu đầy ");
            return false; // máu đầy, không nhặt
        }

        currentHealth += amount;
        audioSourcePlayerSFX.PlayOneShot(addHealthClip);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
        // print("Thêm máu");

        return true; // Nhặt thành công
    }
    public void UpdateHealthUI()
    {
        healthFill.fillAmount = (float)currentHealth / maxHealth;

        healthText.text = "HP: " + maxHealth + " / " + currentHealth;

    }
    void Die()
    {
        isDead = true;
        anima.SetTrigger("death");
        audioSourcePlayerSFX.PlayOneShot(dieClip);
        StartCoroutine(GameOverDelay());
    }


    IEnumerator GameOverDelay()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        FullUI gameOver = FindFirstObjectByType<FullUI>();
        ZombieSpawner spawner = FindFirstObjectByType<ZombieSpawner>();

        if (gameOver != null && spawner != null)
        {
            gameOver.ShowGameOver(spawner.zombiesKilled, spawner.totalZombieToSpawn);
            gameObject.SetActive(false);
        }
    }

}
