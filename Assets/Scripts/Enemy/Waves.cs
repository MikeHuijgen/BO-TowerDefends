using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private int wave;
    [SerializeField] private int enemysIncreasement;
    private EnemySpawner EnemySpawner;

    private void Start()
    {
        EnemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void GoToNextWave()
    {
        wave++;
        EnemySpawner.StartNextWave(enemysIncreasement);
    }
}
