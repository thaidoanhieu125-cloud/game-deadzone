using System.ComponentModel;
using UnityEngine;

public class SupportPack_Map5 : MonoBehaviour
{
    public ItemType itemType;
    public int healtAmount = 30;
    public int ammoAmount = 10;
    public int goldAmount = 10;
    public AudioClip pickupSound;
    private GoldManager_Map5 goldManager;

    private void Start()
    {
        // Cập nhật tìm kiếm theo class Map 5
        goldManager = FindFirstObjectByType<GoldManager_Map5>();
    }

    public enum ItemType
    {
        Health,
        Ammo,
        Gold
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        // Cập nhật tham chiếu tới các class _Map5
        PlayerHealth_Map5 health = collision.GetComponent<PlayerHealth_Map5>();
        GunShoot_Map5 gun = collision.GetComponentInChildren<GunShoot_Map5>();
        bool pickedUp = false;

        switch (itemType)
        {
            case ItemType.Health:
                if (health != null && health.addHeal(healtAmount))
                {
                    pickedUp = true;
                }
                break;

            case ItemType.Ammo:
                if (gun != null && gun.AddAmmo(ammoAmount))
                {
                    pickedUp = true;
                }
                break;

            case ItemType.Gold:
                if (goldManager != null && goldManager.AddGold(goldAmount))
                {
                    pickedUp = true;
                }
                break;
        }

        if (pickedUp)
        {
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}