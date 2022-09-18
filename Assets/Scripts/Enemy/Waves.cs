using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawner;
    [SerializeField] private int amountOfEnemys;
    [SerializeField] private int enemysSpawned;
    [SerializeField] private float timeBetweenNextSpawn;

    private void Update()
    {
        SpawnEnemys();
    }

    private void SpawnEnemys()
    {
        timeBetweenNextSpawn -= Time.deltaTime;
        if (amountOfEnemys > enemysSpawned && timeBetweenNextSpawn <= 0)
        {
            Instantiate(enemyPrefab, spawner.position, Quaternion.identity);
            timeBetweenNextSpawn += 2;
            enemysSpawned++;
        }
    }
}
