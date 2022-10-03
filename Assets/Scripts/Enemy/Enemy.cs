using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int balloonHealth;
    [SerializeField] private int enemyDieGold;
    [SerializeField] public float inGameTime = 0f;
    [SerializeField] private Mesh balloonMesh;
    [SerializeField] private Mesh moabMesh;

    [System.Serializable]
    public class BalloonLayers
    {
        public int balloonHealthId;
        public BalloonLayer balloonLayer;
    }

    public BalloonLayers[] balloonLayers;
    private Dictionary<int,BalloonLayer> balloonDictionary = new Dictionary<int, BalloonLayer>();
    private BalloonLayer currentBalloonLayer;


    private bool isDisable;

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
            GetComponent<MeshFilter>().mesh = moabMesh;
            GetComponent<Renderer>().material = balloonLayer.balloonMaterial;
            this.balloonHealth = balloonLayer.BalloonHealth;
        }
        else
        {
            GetComponent<MeshFilter>().mesh = balloonMesh;
            GetComponent<Renderer>().material = balloonLayer.balloonMaterial;
            GetComponent<Renderer>().material.color = balloonLayer.balloonColor;
            this.balloonHealth = balloonLayer.BalloonHealth;
        }

        transform.localScale = balloonLayer.balloonScale;
        followWaypoint.ChangeBalloonSpeed(balloonLayer.BalloonSpeed);
        Debug.Log(balloonHealth);
    }

    private void OnEnable()
    {
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
        // this is the timer that the balloon is active in the game
        if (!isDisable)
        {
            inGameTime += Time.deltaTime;
        }
    }

    public void DecreaseHealth(int amount, Transform tower)
    {
        balloonHealth -= amount;

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
        // it change how the balloon work by using a sort preset from the dictionary
        if (!balloonDictionary.ContainsKey(balloonHealth)) { return; }
        if(currentBalloonLayer.specialBalloon)
        {
            GetComponent<MeshFilter>().mesh = balloonMesh;
        }
        GetComponent<Renderer>().material.color = balloonDictionary[balloonHealth].balloonColor;
        followWaypoint.ChangeBalloonSpeed(balloonDictionary[balloonHealth].BalloonSpeed);
    }
}
