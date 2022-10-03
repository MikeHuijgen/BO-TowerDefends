using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Waves : MonoBehaviour
{
    [SerializeField] private int wave;
    [SerializeField] private int maxWave;
    [SerializeField] private int moneyIncreaseAmount; 
    [SerializeField] private TMP_Text waveCounter;

    [SerializeField] private List<WaveScriptableObject> waves = new List<WaveScriptableObject>();
    private EnemySpawner EnemySpawner;
    private Bank bank;

    private bool canSpawnNextWave = false;

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
        EnemySpawner = FindObjectOfType<EnemySpawner>();
        GoToNextWave();
    }

    public void StartNextWave()
    {
        wave++;
        waveCounter.text = $"{wave}/{maxWave}";
        canSpawnNextWave = true;
        GoToNextWave();
    }

    public void GoToNextWave()
    {
        if (wave < maxWave)
        {
            wave++;
            waveCounter.text = $"{wave}/{maxWave}";
            bank.IncreaseBankAmount(moneyIncreaseAmount);
            EnemySpawner.WaveHasStarted(waves[wave - 1]);
        }
    }
}
