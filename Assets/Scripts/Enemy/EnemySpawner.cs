using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject balloonPrefab;
    [Header("Spanwer Settings")]
    [SerializeField][Range(30,150)] private int enemyPoolSize;
    [SerializeField][Range(0.1f, 30f)] private float betweenSpawnTime;
    [SerializeField] private List<GameObject> balloonPool = new List<GameObject>();

    public int enemysLeft;
    private int enemysHaveSpawned;
    public int waveBalloonsIndex;

    private bool canSpawn = true;
    private Waves waves;
    private Transform towerParent;

    private WaveScriptableObject currentWave;

    // Het spawnen via een scriptable object werkt alleen ze spawnen nog allemaal tegelijkertijd dat moet nog opgelost worden 
    // daarna moet ik nog ff de code wat cleanen zodat het weer wat duidelijker is

    private void Awake()
    {
        waves = FindObjectOfType<Waves>();
        PopulatePool();
    }

    private void Update()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void PopulatePool()
    {
        //makes a pool of enemys that then can be turned on and off

        for (int i = 0; i < enemyPoolSize; i++) 
        {
            GameObject newBalloon = Instantiate(balloonPrefab, transform);
            newBalloon.SetActive(false);
            balloonPool.Add(newBalloon);
        }
    }

    public void StartNextWave(WaveScriptableObject wave)
    {
        canSpawn = true;
        currentWave = wave;
        betweenSpawnTime = currentWave.timeBetweenSpawn;
    }

    // Spawn of the balloons

    IEnumerator SpawnEnemy()
    {
        if (canSpawn)
        {
            canSpawn = false;
            yield return new WaitForSeconds(betweenSpawnTime);
            EnableBalloons();
        }

    }

    private void EnableBalloons()
    {
        canSpawn = true;

        //zorgt er voor dat de enemys geactiveerd worden na de spawn time
        for (int i = 0;i < balloonPool.Count;i++)
        {
            if (!balloonPool[i].activeInHierarchy && currentWave.balloons[waveBalloonsIndex].amount > enemysHaveSpawned)
            {
                enemysHaveSpawned++;
                enemysLeft++;

                balloonPool[i].GetComponent<Enemy>().SetUpBalloon(currentWave.balloons[waveBalloonsIndex].balloonLayer);
                balloonPool[i].SetActive(true);
                balloonPool[i].GetComponent<EnemyFollowWaypoint>().enemySpawnedIn = true;
                return;
            }
        }

        if (currentWave.balloons[waveBalloonsIndex].amount == enemysHaveSpawned && waveBalloonsIndex < currentWave.balloons.Count - 1)
        {
            enemysHaveSpawned = 0;
            waveBalloonsIndex++;
        }
        else if (waveBalloonsIndex == currentWave.balloons.Count - 1 && enemysLeft <= 0)
        {          
            AllBalloonsDied();
        }
    }

    private void AllBalloonsDied()
    {
        enemysHaveSpawned = 0;
        waveBalloonsIndex = 0;
        canSpawn = false;
        waves.GoToNextWave();
    }
}
