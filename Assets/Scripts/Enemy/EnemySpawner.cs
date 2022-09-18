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

    GameObject[] pool;

    private void Awake()
    {
        enemyPoolSize = maxEnemysInWave;
        PopulatePool();
    }

    private void Update()
    {
        StartCoroutine(SpawnEnemy());
        AllEnemysDied();
    }

    private void PopulatePool()
    {
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
            EnableEnemyInPool();
            enemysHaveSpawned++;
            enemysLeft++;
            Debug.Log(enemysHaveSpawned);
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
                return;
            }
        }
    }

    private void AllEnemysDied()
    {
        
    }
}
