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


    void Start()
    {
        StartCoroutine(IncreaseDifficulty());
        Invoke("SpawnEnemy", 1f);

    }

    void SpawnEnemy()
    {
        if(!Tim.showingUI)
        {
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
    }
    IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(15f);
        minTimeBtwSpawn = 0.75f;
        maxTimeBtwSpawn = 1f;
        Debug.Log("increased difficulty 2");
        yield return new WaitForSeconds(20f);
        minTimeBtwSpawn = 0.5f;
        maxTimeBtwSpawn = 1f;
        Debug.Log("increased difficulty 2");
        yield return new WaitForSeconds(20f);
        minTimeBtwSpawn = 1.25f;
        maxTimeBtwSpawn = 2.25f;
        Debug.Log("increased difficulty 3");
        yield return new WaitForSeconds(20f);
        minTimeBtwSpawn = 1.1f;
        maxTimeBtwSpawn = 2.1f;
        Debug.Log("increased difficulty 4");
        yield return new WaitForSeconds(20f);
        minTimeBtwSpawn = 1f;
        maxTimeBtwSpawn = 1.55f;
        Debug.Log("increased difficulty 5");
        yield return new WaitForSeconds(20f);
        minTimeBtwSpawn = 0.55f;
        maxTimeBtwSpawn = 1f;
        Debug.Log("increased difficulty 6");
        yield return new WaitForSeconds(30f);
        minTimeBtwSpawn = 0.5f;
        maxTimeBtwSpawn = 0.1f;
        Debug.Log("increased difficulty 7");

    }

    //Code Appropiatied from https://ldjam.com/events/ludum-dare/46/slimekeep
}