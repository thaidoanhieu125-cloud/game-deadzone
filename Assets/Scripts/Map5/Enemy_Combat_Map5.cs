using UnityEngine;

public class Enemy_Combat_Map5 : MonoBehaviour
{
    public int damage = 1;

    private bool playerInRange = false;
    private PlayerHealth_Map5 playerHealth; // Cập nhật tham chiếu tới PlayerHealth_Map5

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            // Đảm bảo script trên Player cũng đã đổi tên thành PlayerHealth_Map5
            playerHealth = collision.GetComponent<PlayerHealth_Map5>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;

            playerHealth = null;
        }
    }

    public void DealDamage()
    {
        if (playerInRange && playerHealth != null)
        {
            playerHealth.ChangeHealth(-damage);
        }
    }
}