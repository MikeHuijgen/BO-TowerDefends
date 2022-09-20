using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Waves : MonoBehaviour
{
    [SerializeField] private int wave;
    [SerializeField] private int maxWave;
    [SerializeField] private int enemysIncreasement;
    [SerializeField][Range(2,10)] public int maxEnemysInFirstWave;
    [SerializeField] private int moneyIncreaseAmount; 
    [SerializeField] private TMP_Text waveCounter;
    private EnemySpawner EnemySpawner;
    private Bank bank;

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
        EnemySpawner = FindObjectOfType<EnemySpawner>();
        wave++;
        waveCounter.text = $"{wave}/{maxWave}";
    }

    public void GoToNextWave()
    {
        if (wave < maxWave)
        {
            wave++;
            waveCounter.text = $"{wave}/{maxWave}";
            EnemySpawner.StartNextWave(enemysIncreasement);
            bank.IncreaseBankAmount(moneyIncreaseAmount);
        }
        else
        {
            Debug.Log("You won");
        }
    }
}
