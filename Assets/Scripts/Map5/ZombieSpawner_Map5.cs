using UnityEngine;
using System.Collections;
using TMPro;

public class ZombieSpawner_Map5 : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public float spawnDelay = 2f;
    public int maxZombieAlive = 10;
    public int totalZombieToSpawn = 20;

    public int zombieAlive = 0;
    public int spawnedZombie = 0;
    public int zombiesKilled = 0;

    public GameObject boss;
    public GameObject bossWarning;
    public GameObject healthBossUI;
    public TMP_Text zombieText;

    private bool bossSpawned = false;

    void Start()
    {
        ZombieAliveText();
        StartCoroutine(SpawnZombie());
    }

    void Update()
    {
        // Kiểm tra điều kiện để gọi Boss
        if (!bossSpawned && spawnedZombie >= totalZombieToSpawn && zombieAlive <= 0)
        {
            StartCoroutine(SpawnBoss());
        }
    }

    IEnumerator SpawnZombie()
    {
        while (spawnedZombie < totalZombieToSpawn)
        {
            if (zombieAlive < maxZombieAlive)
            {
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(
                    zombiePrefab,
                    randomPoint.position,
                    Quaternion.identity
                );

                zombieAlive++;
                spawnedZombie++;
                ZombieAliveText(); // Cập nhật lại UI sau mỗi lần tạo zombie
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SpawnBoss()
    {
        bossSpawned = true;

        if (bossWarning != null)
            bossWarning.SetActive(true);

        yield return new WaitForSeconds(3f);

        if (bossWarning != null)
            bossWarning.SetActive(false);

        if (boss != null)
            boss.SetActive(true);

        if (healthBossUI != null)
            healthBossUI.SetActive(true);
    }

    public void ZombieAliveText()
    {
        if (zombieText != null)
        {
            int zombieRemaining = totalZombieToSpawn - zombiesKilled;
            // Đảm bảo không hiển thị số âm
            zombieText.text = Mathf.Max(0, zombieRemaining).ToString();
        }
    }
}