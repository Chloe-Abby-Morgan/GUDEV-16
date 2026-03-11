using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    GameObject currentPoint;
    int index;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float minTimeBtwSpawn;
    [SerializeField] private float maxTimeBtwSpawn;
    [SerializeField] private bool canSpawn = false;
    [SerializeField] private float spawnTime;
    [SerializeField] private int enemiesInRoom;
    [SerializeField] private bool spawnerDone;
    public TimingManager Tim;
    private bool spawning=true;


    void Start()
    {
        StartCoroutine(IncreaseDifficulty());
        Invoke("SpawnEnemy", 1f);

    }

    void SpawnEnemy()
    {
        if(!Tim.showingUI)
        {
        spawning = true;
        index = Random.Range(0, spawnPoints.Length);
        currentPoint = spawnPoints[index];
        float spawnTime = Random.Range(minTimeBtwSpawn, maxTimeBtwSpawn);

        if (canSpawn)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], currentPoint.transform.position, Quaternion.identity);
            enemiesInRoom++;
        }

        Invoke("SpawnEnemy", spawnTime);
        }
        else
        {
            spawning = false;
        }
    }
    private void Update()
    {
        if (canSpawn && !Tim.showingUI)
        {
            spawnTime -= Time.fixedDeltaTime;
            if (spawnTime <= 0)
            {
                canSpawn = false;
            }
        }
        if(!spawning)
        {
            SpawnEnemy();
        }
    }
    IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(15f);
        minTimeBtwSpawn = 3f;
        maxTimeBtwSpawn = 4f;
        yield return new WaitForSeconds(30f);
        minTimeBtwSpawn = 3.25f;
        maxTimeBtwSpawn = 3.75f;
        Debug.Log("increased difficulty 3");
        yield return new WaitForSeconds(40f);
        minTimeBtwSpawn = 3f;
        maxTimeBtwSpawn = 3.5f;
    }

    //Code Appropiatied from https://ldjam.com/events/ludum-dare/46/slimekeep
}