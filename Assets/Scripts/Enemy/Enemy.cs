using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int balloonHealth;
    [SerializeField] public bool CamoBalloon;


    [System.Serializable]
    public class BalloonLayers
    {
        public int balloonHealthId;
        public BalloonLayer balloonLayer;
    }

    public BalloonLayers[] balloonLayers;
    private Dictionary<int,BalloonLayer> balloonDictionary = new Dictionary<int, BalloonLayer>();
    public BalloonLayer currentBalloonLayer;


    private int goldAmount;
    private int lastHealth;
    private bool isDisable;

    private Bank bank;
    private EnemySpawner enemySpawner;
    private EnemyFollowWaypoint followWaypoint;
    private EnemyCounter enemyCounter;
    private Transform balloonPopeffectTransform;


    private void Awake()
    {
        //Fill up the dictionary in the beginning of the game
        foreach (var layers in balloonLayers)
        {
            balloonDictionary.Add(layers.balloonHealthId, layers.balloonLayer);     
        }
        enemySpawner = FindObjectOfType<EnemySpawner>();
        followWaypoint = FindObjectOfType<EnemyFollowWaypoint>();
        enemyCounter = FindObjectOfType<EnemyCounter>();
        bank = FindObjectOfType<Bank>();
        balloonPopeffectTransform = GameObject.FindGameObjectWithTag("BalloonPop").transform;
    }

    public void SetUpBalloon(BalloonLayer balloonLayer)
    {
        currentBalloonLayer = balloonLayer;
        // this set up the balloon when it get activated
        if (balloonLayer.MOABBalloon)
        {
            GetComponent<MeshFilter>().mesh = balloonLayer.balloonMesh;
            GetComponent<Renderer>().material = balloonLayer.balloonMaterial;
        }
        else
        {
            GetComponent<MeshFilter>().mesh = balloonLayer.balloonMesh;
            GetComponent<Renderer>().material = balloonLayer.balloonMaterial;
            GetComponent<Renderer>().material.color = balloonLayer.balloonColor;
        }

        CamoBalloon = balloonLayer.camoBalloon;
        this.balloonHealth = balloonLayer.BalloonHealth;
        transform.localScale = balloonLayer.balloonScale;
        followWaypoint.ChangeBalloonSpeed(balloonLayer.BalloonSpeed);
    }

    private void OnEnable()
    {
        isDisable = false;
    }

    private void OnDisable()
    {
        isDisable = true;
    }

    public void DecreaseHealth(int amount, Transform tower)
    {
        if (isDisable) { return; }
        balloonPopeffectTransform.GetComponent<AudioSource>().Stop();
        balloonPopeffectTransform.GetComponent<AudioSource>().Play();
        lastHealth = balloonHealth;
        balloonHealth -= amount;
        BalloonGotHit();

        if (balloonHealth > 0)
        {
            goldAmount = amount;
            bank.IncreaseBankAmount(goldAmount);
        }

        if (balloonHealth <= 0)
        {
            balloonHealth = 0;
            if (lastHealth > 1)
            {
                goldAmount = lastHealth - balloonHealth;
                bank.IncreaseBankAmount(goldAmount);
            }
            else
            {
                goldAmount = 1;
                bank.IncreaseBankAmount(goldAmount);
            }
            tower.parent.BroadcastMessage("EnemyGotKilledByTower", this.transform);
            enemyCounter.DecreaseEnemyCounter();
            enemySpawner.enemysLeft--;
            GetComponent<EnemyFollowWaypoint>().totalDistanceTraveled = 0;
            gameObject.SetActive(false);
        }
    }

    private void BalloonGotHit()
    {
        // it change how the balloon work by using a sort preset from the dictionary
        if (!balloonDictionary.ContainsKey(balloonHealth)) { return; }
        if(currentBalloonLayer.MOABBalloon)
        {
            GetComponent<MeshFilter>().mesh = balloonDictionary[balloonHealth].balloonMesh;
            GetComponent<Renderer>().material = balloonDictionary[balloonHealth].balloonMaterial;
        }
        transform.localScale = balloonDictionary[balloonHealth].balloonScale;
        GetComponent<Renderer>().material.color = balloonDictionary[balloonHealth].balloonColor;
        followWaypoint.ChangeBalloonSpeed(balloonDictionary[balloonHealth].BalloonSpeed);
        currentBalloonLayer = balloonDictionary[balloonHealth];
    }
}
