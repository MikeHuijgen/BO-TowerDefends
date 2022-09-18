using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject enemyPrefab;
    [Header("Spanwer Settings")]
    [SerializeField][Range(1,100)] private int maxEnemysInWave;
    [SerializeField][Range(0.1f, 30f)] private float betweenSpawnTime;
    
    private int enemyPoolSize;
    private int enemysHaveSpawned;
    public int enemysLeft;

    private bool canSpawn = true;

    private Waves waves;
    private GameObject[] pool;

    private void Awake()
    {
        enemyPoolSize = maxEnemysInWave;
        waves = FindObjectOfType<Waves>();
        PopulatePool();
    }

    private void Update()
    {
        StartCoroutine(SpawnEnemy());
        CheckIfEveryEnemyDied();
    }

    private void PopulatePool()
    {
        //makes a pool of enemys that then can be turned on and off
        pool = new GameObject[enemyPoolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemy()
    {
        if (enemysHaveSpawned < maxEnemysInWave && canSpawn)
        {
            canSpawn = false;
            enemysLeft++;
            enemysHaveSpawned++;
            EnableEnemyInPool();
            yield return new WaitForSeconds(betweenSpawnTime);
            canSpawn = true;
        }
    }

    private void EnableEnemyInPool()
    {    
        //zorgt er voor dat de enemys geactiveerd worden na de spawn time
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                pool[i].GetComponent<Enemy>().enemySpawnedIn = true;
                return;
            }
        }
    }

    private void CheckIfEveryEnemyDied()
    {
        if(enemysLeft <= 0)
        {
            Debug.Log("Next wave");
            enemysHaveSpawned = 0;
            waves.GoToNextWave();
        }
    }

    public void StartNextWave(int enemyIncreasement)
    {
        maxEnemysInWave += enemyIncreasement;
        enemyPoolSize = maxEnemysInWave;
        PopulatePool();
    }
}
