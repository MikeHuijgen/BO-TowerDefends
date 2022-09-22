using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private int enemyDieGold;
    [SerializeField] public float inGameTime = 0f;
    [SerializeField] private Gradient healthGradient;
    public int enemyId;
    private EnemySpawner enemySpawner;

    private bool isDisable;

    private void OnEnable()
    {
        inGameTime = 0;
        isDisable = false;
    }

    private void OnDisable()
    {
        isDisable = true;
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        CheckInGameTime();
    }

    private void CheckInGameTime()
    {
        if (!isDisable)
        {
            inGameTime += Time.deltaTime;
        }
    }

    public void DecreaseHealth(float amount)
    {
        enemyHealth -= amount;

        if ( enemyHealth <= 0)
        {
            Bank bank = FindObjectOfType<Bank>();
            bank.IncreaseBankAmount(enemyDieGold);
            enemySpawner.enemysLeft--;
            gameObject.SetActive(false);
        }
    }
}
