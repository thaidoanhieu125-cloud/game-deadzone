

using System.ComponentModel;
using UnityEngine;

public class SupportPack_Map3 : MonoBehaviour
{
    public ItemType itemType;
    public int healtAmount = 30;
    public int ammoAmount = 10;

    public int goldAmount = 10;
    private GoldManager_Map3 goldManager;
    // Kéo file âm thanh (.wav) nhặt đồ vào ô này
    [Header("Audio Settings")]
    public AudioClip pickupSound;

    private void Start()
    {
            goldManager = FindFirstObjectByType<GoldManager_Map3>();
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

        PlayerHealth_Map3 health = collision.GetComponent<PlayerHealth_Map3>();
        GunShoot_Map3 gun = collision.GetComponentInChildren<GunShoot_Map3>();
        bool pickedUp = false;

        switch (itemType)
        {
            case ItemType.Health:
                if (health.addHeal(healtAmount))
                {
                    pickedUp = true;
                }
                break;

            case ItemType.Ammo:
                if (gun.AddAmmo(ammoAmount))
                {
                    pickedUp = true;
                }
                break;

            case ItemType.Gold:
                if (goldManager.AddGold(goldAmount))
                {
                    pickedUp = true;
                }
                break;
        }

        if (pickedUp)
        {


            // Lệnh này tự sinh ra loa ảo tại vị trí nhặt, phát xong tự xóa loa
            // =======================================================
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            //
            Destroy(gameObject);
        }
    }

}