using UnityEngine;
using UnityEngine.UI;

public class ZombieArrow : MonoBehaviour
{
    public Transform player;
    public RectTransform arrow;
    public GameObject boss;
    public ZombieSpawner spawner;

    void Update()
    {
        Transform target = GetTarget();

        if (target == null)
        {
            arrow.gameObject.SetActive(false);
            return;
        }

        arrow.gameObject.SetActive(true);

        Vector2 dir =
            target.position - player.position;

        float angle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        arrow.rotation =
            Quaternion.Euler(0, 0, angle - 90);
    }

    Transform GetTarget()
    {
        GameObject[] zombies =
            GameObject.FindGameObjectsWithTag("Zombie");

        float nearestDistance = Mathf.Infinity;
        Transform nearestZombie = null;

        foreach (GameObject zombie in zombies)
        {
            float distance =
                Vector2.Distance(
                    player.position,
                    zombie.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestZombie = zombie.transform;
            }
        }

        if (nearestZombie != null)
            return nearestZombie;

        if (spawner.zombieAlive <= 0)
            return boss.transform;

        return null;
    }
}