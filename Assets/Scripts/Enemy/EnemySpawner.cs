using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject enemyPrefab;
    [Header("Spanwer Settings")]
    [SerializeField][Range(30,150)] private int enemyPoolSize;
    [SerializeField][Range(2, 50)] private int maxEnemysOnScreen;
    [SerializeField][Range(0.1f, 30f)] private float betweenSpawnTime;
    [SerializeField] private List<GameObject> enemyPool = new List<GameObject>();
    
    private int maxEnemysInWave;
    private int enemyID;
    private int enemysHaveSpawned;
    public int enemysLeft;

    private bool canSpawn = true;

    private Waves waves;

    private void Awake()
    {
        enemyID = 0;
        waves = FindObjectOfType<Waves>();
        maxEnemysInWave = waves.maxEnemysInFirstWave;
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

        for (int i = 0; i < enemyPoolSize; i++) 
        {
            enemyID++;
            GameObject newEnemy = Instantiate(enemyPrefab, transform);
            newEnemy.GetComponent<EnemyID>().GiveEnemyIDNumber(enemyID);
            newEnemy.SetActive(false);
            enemyPool.Add(newEnemy);
        }
    }

    IEnumerator SpawnEnemy()
    {
        if (enemysHaveSpawned < maxEnemysInWave && enemysHaveSpawned < maxEnemysOnScreen && canSpawn)
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
        for (int i = 0;i < enemyPool.Count;i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                enemyPool[i].SetActive(true);
                enemyPool[i].GetComponent<EnemyFollowWaypoint>().enemySpawnedIn = true;
                return;
            }
        }
    }

    private void CheckIfEveryEnemyDied()
    {
        if(enemysLeft <= 0 && enemysHaveSpawned > 1)
        {
            canSpawn = false;
            Debug.Log("Next wave");
            enemysHaveSpawned = 0;
            waves.GoToNextWave();
        }
    }

    public void StartNextWave(int increasement)
    {
        maxEnemysInWave += increasement;
        canSpawn = true;
    }
}
