using UnityEngine;

public class Enemy_Combat_Map2 : MonoBehaviour
{
    public int damage = 1;

    private bool playerInRange = false;
    private PlayerHealth_Map2 playerHealth;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            playerHealth = collision.GetComponent<PlayerHealth_Map2>();
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

