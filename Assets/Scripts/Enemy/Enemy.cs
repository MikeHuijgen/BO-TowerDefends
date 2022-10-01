using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int balloonHealth;
    [SerializeField] private int enemyDieGold;
    [SerializeField] public float inGameTime = 0f;

    [System.Serializable]
    public class BalloonStage
    {
        public int key;
        public BalloonInfo balloonInfo;
    }

    public BalloonStage[] balloonStage;
    private Dictionary<int,BalloonInfo> balloonDictionary = new Dictionary<int, BalloonInfo>();


    private bool isDisable;
    private int resetHealth;
    public int balloonKey;

    private EnemySpawner enemySpawner;
    private EnemyFollowWaypoint followWaypoint;


    private void Awake()
    {
        foreach (var balloonStage in balloonStage)
        {
            balloonDictionary.Add(balloonStage.key, balloonStage.balloonInfo);     
        }
        enemySpawner = FindObjectOfType<EnemySpawner>();
        followWaypoint = FindObjectOfType<EnemyFollowWaypoint>();
    }
    public void SetUpBalloon(BalloonInfo balloonInfo, int id)
    {
        balloonKey = id;
        GetComponent<Renderer>().material.color = balloonInfo.balloonColor;
        followWaypoint.ChangeBalloonSpeed(balloonInfo.BalloonSpeed);
        balloonHealth = balloonInfo.BalloonHealth;
    }

    private void OnEnable()
    {
        ResetHealth();
        inGameTime = 0;
        isDisable = false;
    }


    private void OnDisable()
    {
        isDisable = true;
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
        balloonKey -= amount;

        BalloonGotHit();

        if ( balloonHealth <= 0)
        {
            tower.parent.BroadcastMessage("EnemyGotKilledByTower", this.transform);
            Bank bank = FindObjectOfType<Bank>();
            bank.IncreaseBankAmount(enemyDieGold);
            enemySpawner.enemysLeft--;
            gameObject.SetActive(false);
        }
    }

    private void BalloonGotHit()
    {
        if (!balloonDictionary.ContainsKey(balloonKey)) { return; }
        GetComponent<Renderer>().material.color = balloonDictionary[balloonKey].balloonColor;
        followWaypoint.ChangeBalloonSpeed(balloonDictionary[balloonKey].BalloonSpeed);
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
