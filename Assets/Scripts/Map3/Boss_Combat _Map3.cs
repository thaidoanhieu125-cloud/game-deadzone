
using UnityEngine;

public class Boss_Combat_Map3 : MonoBehaviour
{

    public int damage = 1;

    private bool playerInRange = false;
    private PlayerHealth_Map3 playerHealth;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            playerHealth = collision.GetComponent<PlayerHealth_Map3>();
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
