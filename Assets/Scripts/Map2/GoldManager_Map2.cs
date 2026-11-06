using TMPro;
using UnityEngine;

public class GoldManager_Map2 : MonoBehaviour
{
    public int gold = 0;
    public TMP_Text goldText;
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

