using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int balloonHealth;
    [SerializeField] private int enemyDieGold;
    [SerializeField] public float inGameTime = 0f;
    [SerializeField] private List<Color> balloonColor = new List<Color>();
    public int enemyId;
    private EnemySpawner enemySpawner;

    private bool isDisable;
    private int resetHealth;

    private void OnEnable()
    {
        ResetHealth();
        inGameTime = 0;
        isDisable = false;
        CheckBalloonColor();
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

    public void DecreaseHealth(int amount, Transform tower)
    {
        balloonHealth -= amount;
        CheckBalloonColor();

        if ( balloonHealth <= 0)
        {
            tower.parent.BroadcastMessage("EnemyGotKilledByTower", this.transform);
            Bank bank = FindObjectOfType<Bank>();
            bank.IncreaseBankAmount(enemyDieGold);
            enemySpawner.enemysLeft--;
            gameObject.SetActive(false);
        }
    }

    private void CheckBalloonColor()
    {
        for (int i = 0; i < balloonColor.Count; i++)
        {
            if (i == balloonHealth - 1)
            {
                GetComponent<Renderer>().material.color = balloonColor[i];
            }
        }
    }

    private void ResetHealth()
    {
        if(resetHealth < balloonHealth)
        {
            resetHealth = balloonHealth;
        }
        else
        {
            balloonHealth = resetHealth;
        }
    }
}
