using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth_Map5 : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public TMP_Text healthText;
    public Animator anima;
    private bool isDead = false;
    public Image healthFill;

    void Start()
    {
        UpdateHealthUI();
    }

    public void ChangeHealth(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();
        anima.SetTrigger("hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public bool addHeal(int amount)
    {
        if (currentHealth >= maxHealth)
        {
            return false; // Máu đầy, không nhặt thêm
        }

        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
        return true; // Nhặt thành công
    }

    public void UpdateHealthUI()
    {
        if (healthFill != null)
            healthFill.fillAmount = (float)currentHealth / maxHealth;

        if (healthText != null)
            healthText.text = "HP: " + maxHealth + " / " + currentHealth;
    }

    void Die()
    {
        isDead = true;
        anima.SetTrigger("death");
        StartCoroutine(GameOverDelay());
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        // Cập nhật tham chiếu tới các class _Map5 mới
        FullUI_Map5 gameOver = FindFirstObjectByType<FullUI_Map5>();
        ZombieSpawner_Map5 spawner = FindFirstObjectByType<ZombieSpawner_Map5>();

        if (gameOver != null && spawner != null)
        {
            gameOver.ShowGameOver(spawner.zombiesKilled, spawner.totalZombieToSpawn);
            gameObject.SetActive(false);
        }
    }

}