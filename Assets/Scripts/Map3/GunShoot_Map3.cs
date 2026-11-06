

   using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class GunShoot_Map3  : MonoBehaviour
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


    // 👉 1. THÊM CÁC BIẾN ÂM THANH Ở ĐÂY
    // =======================================================
    [Header("Audio Settings")]
    public AudioSource gunAudioSource;   // Biến để kéo cái AudioSource thứ 2 vào
    public AudioClip gunSoundClip;       // Biến để kéo file tiếng súng .wav vào
    void Start()
    {
        UpdateAmmoUI();
    }
    void Update()
    {
        //fix vào shop ko bắn đạn
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    public bool AddAmmo(int amount)
    {
        if (currentAmmo >= maxAmmo)
        {
            // print("đầy đạn");
            return false; // Đạn đầy không nhặt
        }

        currentAmmo += amount;

        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        UpdateAmmoUI();
        // print("Thêm đạn");

        return true; // Nhặt thành công
    }
    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Hết đạn");
            return;
        }
        GameObject bullet = Instantiate(
           bulletPrefab,
           firePoint.position,
           firePoint.rotation
       );
        bullet.GetComponent<Bullet_Map3>().damage = damage;

        anima.SetTrigger("shoot");

        // Sử dụng PlayOneShot để âm thanh nổ đè lên nhau tự nhiên khi bắn liên tục
        // =======================================================
        if (gunAudioSource != null && gunSoundClip != null)
        {
            gunAudioSource.PlayOneShot(gunSoundClip);
        }
        currentAmmo--;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        UpdateAmmoUI();
    }
    public void UpdateAmmoUI()
    {
        ammoText.text = maxAmmo + " / " + currentAmmo;
    }
}