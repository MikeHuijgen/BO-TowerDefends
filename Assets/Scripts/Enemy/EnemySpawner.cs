using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject redBalloonPrefab;
    [SerializeField] private GameObject blueBalloonPrefab;
    [Header("Spanwer Settings")]
    [SerializeField][Range(30,150)] private int enemyPoolSize;
    [SerializeField][Range(2, 50)] private int maxEnemysOnScreen;
    [SerializeField][Range(0.1f, 30f)] private float betweenSpawnTime;
    [SerializeField] private List<GameObject> redBalloonPool = new List<GameObject>();
    [SerializeField] private List<GameObject> blueBalloonPool = new List<GameObject>();

    //Balloons spawned
    private int redBalloonsSpawned;
    private int blueBalloonsSpawned;
    public int enemysLeft;

    //Balloon Amount
    private int redBalloonAmount;
    private int blueBalloonAmount;

    [SerializeField] private bool spawnRedBalloons = false;
    [SerializeField] private bool spawnBlueBalloons = false;

    private bool canSpawn = true;
    private bool spawnNextBalloon = true;

    private Waves waves;


    [SerializeField] private List<WaveScriptableObject> wave = new List<WaveScriptableObject>();

    // Het spawnen via een scriptable object werkt alleen ze spawnen nog allemaal tegelijkertijd dat moet nog opgelost worden 
    // daarna moet ik nog ff de code wat cleanen zodat het weer wat duidelijker is

    private void Awake()
    {
        waves = FindObjectOfType<Waves>();
        PopulatePool();
    }

    private void Start()
    {
        ChecktypeBalloons();
    }

    private void ChecktypeBalloons()
    {
        for (int i = 0; i < wave[0].balloons.Count; i++)
        {
            if (redBalloonPool[0].name == wave[0].balloons[i].balloon.name && !spawnRedBalloons)
            {
                redBalloonAmount = wave[0].balloons[i].amount;
                spawnRedBalloons = true;
                Debug.Log(redBalloonAmount);
            }

            if (blueBalloonPool[0].name == wave[0].balloons[i].balloon.name && !spawnBlueBalloons)
            {
                blueBalloonAmount = wave[0].balloons[i].amount;
                spawnBlueBalloons = true;
                Debug.Log(blueBalloonAmount);
            }
        }
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
            GameObject newEnemy = Instantiate(redBalloonPrefab, transform);
            newEnemy.name = redBalloonPrefab.name;
            newEnemy.SetActive(false);
            redBalloonPool.Add(newEnemy);
        }

        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject newEnemy = Instantiate(blueBalloonPrefab, transform);
            newEnemy.name = blueBalloonPrefab.name;
            newEnemy.SetActive(false);
            blueBalloonPool.Add(newEnemy);
        }
    }

    IEnumerator SpawnEnemy()
    {
        if (canSpawn)
        {
            canSpawn = false;
            yield return new WaitForSeconds(betweenSpawnTime);
            EnableRedBalloons();
            EnableBlueBalloons();
            canSpawn = true;
        }

    }

    private void EnableRedBalloons()
    {
        //zorgt er voor dat de enemys geactiveerd worden na de spawn time
        for (int i = 0;i < redBalloonPool.Count;i++)
        {
            if (!redBalloonPool[i].activeInHierarchy && spawnRedBalloons && redBalloonAmount > redBalloonsSpawned)
            {
                enemysLeft++;
                redBalloonsSpawned++;
                redBalloonPool[i].SetActive(true);
                redBalloonPool[i].GetComponent<EnemyFollowWaypoint>().enemySpawnedIn = true;
                return;
            }
        }
    }

    private void EnableBlueBalloons()
    {
        //zorgt er voor dat de enemys geactiveerd worden na de spawn time
        for (int i = 0; i < redBalloonPool.Count; i++)
        {
            if (!blueBalloonPool[i].activeInHierarchy && spawnBlueBalloons && blueBalloonAmount > blueBalloonsSpawned)
            {
                enemysLeft++;
                blueBalloonsSpawned++;
                blueBalloonPool[i].SetActive(true);
                blueBalloonPool[i].GetComponent<EnemyFollowWaypoint>().enemySpawnedIn = true;
                return;
            }
        }
    }
}
