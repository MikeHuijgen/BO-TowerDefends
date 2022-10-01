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

    private bool canSpawn = true;

    private Waves waves;


    [SerializeField] private List<WaveScriptableObject> wave = new List<WaveScriptableObject>();


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

    // Spawn of the balloons

    IEnumerator SpawnEnemy()
    {
        if (canSpawn)
        {
            canSpawn = false;
            yield return new WaitForSeconds(betweenSpawnTime);
            EnableBalloons();
            canSpawn = true;
        }

    }

    private void EnableBalloons()
    {
        //zorgt er voor dat de enemys geactiveerd worden na de spawn time
        for (int i = 0;i < balloonPool.Count;i++)
        {
            if (!balloonPool[i].activeInHierarchy)
            {
                balloonPool[i].GetComponent<Enemy>().SetUpBalloon(wave[0].balloons[0].balloonType, wave[0].balloons[0].balloonType.balloonKey);
                enemysLeft++;
                balloonPool[i].SetActive(true);
                balloonPool[i].GetComponent<EnemyFollowWaypoint>().enemySpawnedIn = true;
                return;
            }
        }
    }
}
