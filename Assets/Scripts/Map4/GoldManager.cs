using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int gold = 0 ;
    public TMP_Text goldText;
    public AudioSource audioSource;
    void Start()
    {
        UpdateUI();
    }

    public bool AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
        return true;
    }

    public void UpdateUI()
    {
        goldText.text = gold.ToString();
    }
}
