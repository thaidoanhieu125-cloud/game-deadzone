using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class GunShoot : MonoBehaviour
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
    public AudioSource audioSource;
    public AudioClip gunClip;
    public AudioClip noAmmoClip;
    public AudioClip addAmmoClip;


    void Start()
    {
        UpdateAmmoUI();
        // audioSource.clip = gunClip;
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
        audioSource.PlayOneShot(addAmmoClip);
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
            audioSource.PlayOneShot(noAmmoClip);

            return;
        }
        GameObject bullet = Instantiate(
           bulletPrefab,
           firePoint.position,
           firePoint.rotation
       );
        bullet.GetComponent<Bullet>().damage = damage;

        // audioSource.Stop();
        // audioSource.Play();
        audioSource.PlayOneShot(gunClip);
        anima.SetTrigger("shoot");
        currentAmmo--;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        UpdateAmmoUI();
    }
    public void UpdateAmmoUI()
    {
        ammoText.text = maxAmmo + " / " + currentAmmo;
    }
}