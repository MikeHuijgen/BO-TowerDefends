using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Waves : MonoBehaviour
{
    [SerializeField] private int wave;
    [SerializeField] private int moneyIncreasePerWave; 
    [SerializeField] private TMP_Text waveCounter;

    [SerializeField] private List<WaveScriptableObject> waves = new List<WaveScriptableObject>();
    private EnemySpawner EnemySpawner;
    private Bank bank;

    private int maxWave;

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
        EnemySpawner = FindObjectOfType<EnemySpawner>();
        wave++;
        maxWave = waves.Count;
        waveCounter.text = $"{wave}/{maxWave}";
        EnemySpawner.StartNextWave(waves[wave - 1]);
    }

    public void GoToNextWave()
    {
        if (wave < maxWave)
        {
            wave++;
            Debug.Log(wave);
            waveCounter.text = $"{wave}/{maxWave}";
            bank.IncreaseBankAmount(moneyIncreasePerWave);
            EnemySpawner.StartNextWave(waves[wave - 1]);
        }
        else if(wave >= maxWave)
        {
            PlayerWonTheGame();
        }
    }

    private void PlayerWonTheGame()
    {
        Debug.Log("You won");
    }
}
