using UnityEngine;

public class Boss_Combat : MonoBehaviour
{

    public int damage = 1;

    private bool playerInRange = false;
    private PlayerHealth playerHealth;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            playerHealth = collision.GetComponent<PlayerHealth>();
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
