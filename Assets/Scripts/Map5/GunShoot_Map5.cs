using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class GunShoot_Map5 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Animator anima;
    public float fireRate = 0.3f;
    private float nextFireTime;
    public int maxAmmo = 30;
    public int currentAmmo = 30;
    public int damage = 10;
    public TMP_Text ammoText;

    // --- BỔ SUNG PHẦN ÂM THANH ---
    public AudioSource gunAudioSource; // Kéo AudioSource vào đây
    public AudioClip shootSound;       // Kéo file âm thanh vào đây
    // ----------------------------
    void Start()
    {
        UpdateAmmoUI();
    }

    void Update()
    {
        // Fix lỗi bắn xuyên qua shop
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Mouse.current != null && Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    public bool AddAmmo(int amount)
    {
        if (currentAmmo >= maxAmmo)
        {
            return false;
        }

        currentAmmo += amount;
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        UpdateAmmoUI();
        return true;
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Hết đạn");
            return;
        }
        // --- PHÁT ÂM THANH KHI BẮN ---
        if (gunAudioSource != null && shootSound != null)
        {
            gunAudioSource.PlayOneShot(shootSound);
        }
        // ----------------------------
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        // Cập nhật tham chiếu tới script Bullet_5 mới
        Bullet_5 bulletScript = bullet.GetComponent<Bullet_5>();
        if (bulletScript != null)
        {
            bulletScript.damage = damage;
        }

        anima.SetTrigger("shoot");
        currentAmmo--;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = maxAmmo + " / " + currentAmmo;
        }
    }
}