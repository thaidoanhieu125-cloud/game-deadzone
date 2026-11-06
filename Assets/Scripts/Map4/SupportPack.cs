using System.ComponentModel;
using UnityEngine;

public class SupportPack : MonoBehaviour
{
    public ItemType itemType;
    public int healtAmount = 30;
    public int ammoAmount = 10;

    public int goldAmount = 10;
    private GoldManager goldManager;
    public AudioClip goldPickupClip;

    private void Start()
    {
        goldManager = FindFirstObjectByType<GoldManager>();
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

        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        GunShoot gun = collision.GetComponentInChildren<GunShoot>();
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
                    AudioSource.PlayClipAtPoint(
                        goldPickupClip,
                        transform.position,
                        1f
                    );
                }
                break;
        }

        if (pickedUp)
        {
            if (itemType == ItemType.Gold)
            {
                AudioSource.PlayClipAtPoint(
                    goldPickupClip,
                    transform.position,
                    1f
                );
            }
            Destroy(gameObject);
        }
    }

}
