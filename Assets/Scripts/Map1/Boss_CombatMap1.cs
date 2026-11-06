using UnityEngine;

public class Boss_CombatMap1 : MonoBehaviour
{

    public int damage = 1;

    private bool playerInRange = false;
    private PlayerHealthMap1 playerHealth;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            playerHealth = collision.GetComponent<PlayerHealthMap1>();
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
