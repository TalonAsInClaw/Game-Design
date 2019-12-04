using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    public GameObject EnemySquareGO;
    public GameObject BossGO;

    float whichEnemy = 0f;
    float maxSpawnRateInSeconds = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);

        InvokeRepeating("ChangeEnemyType", 15f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        
        if (!GameObject.FindWithTag("Boss"))
        {
            switch (whichEnemy)
            {
                case 0:
                    {
                        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
                        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
                        break;
                    }
                case 1:
                    {
                        GameObject anEnemy = (GameObject)Instantiate(EnemySquareGO);
                        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
                        break;
                    }
                case 2:
                    {
                        GameObject anEnemy = (GameObject)Instantiate(BossGO);
                        anEnemy.transform.position = new Vector2((min.x + max.x / 2), max.y); // spawn in middle
                        whichEnemy = 0; // reset enemy types for level 2
                        break;
                    }
            }
        }
     
        ScheduleNextEnemySpawn();

    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;

        if(maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInNSeconds = 1f;
        }

        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    void ChangeEnemyType()
    {
        whichEnemy++;
    }
}
