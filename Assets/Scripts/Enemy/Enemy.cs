using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemyHealt;
    private EnemySpawner enemySpawner;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void DecreaseHealth(int amount)
    {
        enemyHealt -= amount;
        enemySpawner.enemysLeft--;
        if ( enemyHealt <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
