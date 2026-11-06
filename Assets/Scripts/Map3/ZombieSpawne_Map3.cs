
 using UnityEngine;
using System.Collections;
using TMPro;

public class ZombieSpawne_Map3  : MonoBehaviour
{
    public GameObject zombiePrefab;

    public Transform[] spawnPoints;

    public float spawnDelay = 2f;

    public int maxZombieAlive = 10;

    public int totalZombieToSpawn = 20;
    // [HideInInspector]
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
        if (!bossSpawned &&
        spawnedZombie >= totalZombieToSpawn &&
        zombieAlive <= 0)
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

                Transform randomPoint =
                    spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(
                    zombiePrefab,
                    randomPoint.position,
                    Quaternion.identity
                );
                zombieAlive++;
                spawnedZombie++;


            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SpawnBoss()
    {
        bossSpawned = true;

        bossWarning.SetActive(true);

        yield return new WaitForSeconds(3f);

        bossWarning.SetActive(false);

        boss.SetActive(true);
        healthBossUI.SetActive(true);
    }

    public void ZombieAliveText()
    {
        int zombieRemaining = totalZombieToSpawn - zombiesKilled;
        zombieText.text = zombieRemaining.ToString();
    }
}