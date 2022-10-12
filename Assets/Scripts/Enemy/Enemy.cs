using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int balloonHealth;
    [SerializeField] private int goldPerPop;

    [System.Serializable]
    public class BalloonLayers
    {
        public int balloonHealthId;
        public BalloonLayer balloonLayer;
    }

    public BalloonLayers[] balloonLayers;
    private Dictionary<int,BalloonLayer> balloonDictionary = new Dictionary<int, BalloonLayer>();
    public BalloonLayer currentBalloonLayer;


    private bool isDisable;

    private Bank bank;
    private EnemySpawner enemySpawner;
    private EnemyFollowWaypoint followWaypoint;


    private void Awake()
    {
        //Fill up the dictionary in the beginning of the game
        foreach (var layers in balloonLayers)
        {
            balloonDictionary.Add(layers.balloonHealthId, layers.balloonLayer);     
        }
        enemySpawner = FindObjectOfType<EnemySpawner>();
        followWaypoint = FindObjectOfType<EnemyFollowWaypoint>();
    }
    public void SetUpBalloon(BalloonLayer balloonLayer)
    {
        currentBalloonLayer = balloonLayer;
        // this set up the balloon when it get activated
        if (balloonLayer.specialBalloon)
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

        this.balloonHealth = balloonLayer.BalloonHealth;
        transform.localScale = balloonLayer.balloonScale;
        followWaypoint.ChangeBalloonSpeed(balloonLayer.BalloonSpeed);
    }

    private void OnEnable()
    {
        isDisable = false;
        bank = FindObjectOfType<Bank>();
    }

    private void OnDisable()
    {
        isDisable = true;
    }

    public void DecreaseHealth(int amount, Transform tower)
    {
        bank.IncreaseBankAmount(goldPerPop);
        balloonHealth -= amount;

        BalloonGotHit();

        if ( balloonHealth <= 0)
        {
            tower.parent.BroadcastMessage("EnemyGotKilledByTower", this.transform);
            enemySpawner.enemysLeft--;
            gameObject.SetActive(false);
        }
    }

    private void BalloonGotHit()
    {
        // it change how the balloon work by using a sort preset from the dictionary
        if (!balloonDictionary.ContainsKey(balloonHealth)) { return; }
        if(currentBalloonLayer.specialBalloon)
        {
            GetComponent<MeshFilter>().mesh = balloonDictionary[balloonHealth].balloonMesh;
        }
        transform.localScale = balloonDictionary[balloonHealth].balloonScale;
        GetComponent<Renderer>().material.color = balloonDictionary[balloonHealth].balloonColor;
        followWaypoint.ChangeBalloonSpeed(balloonDictionary[balloonHealth].BalloonSpeed);
        currentBalloonLayer = balloonDictionary[balloonHealth];
    }
}
