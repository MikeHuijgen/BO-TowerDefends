using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowWaypoint : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int decreasePlayerLife;
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] public float betweenWaypointTime;

    private int amountOfWaypoints = 0;
    public int waypointsPassed = 0;
    private float distanceThreshold = 0.1f;
    private float totalDistance;
    private float distanceTraveled;
    public float precentTraveled;
    private float startTime;

    public bool enemySpawnedIn = false;
    private bool hasFinished = false;
    private bool hasCalculateTheTime = false;

    public Transform currentWaypoint;
    private GameObject waypointParent;
    private EnemySpawner enemySpawner;
    private PlayerHealth playerHealth;

    private void OnEnable()
    {
        startTime = Time.time;
        enemySpawner = GetComponentInParent<EnemySpawner>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        waypointParent = GameObject.FindGameObjectWithTag("waypointParent");
        waypointsPassed = 0;
        FindWaypoints();
        ResetStartPos();
    }

    private void Update()
    {  
        FinishedWaypoints();
        MoveEnemy();
        CheckEnemysPos();
        BalloonProgres();
    }

    private void FindWaypoints()
    {
        //set all waypoints in the list
        waypoints.Clear();

        foreach (Transform child in waypointParent.transform)
        {
            waypoints.Add(child);
        }

        amountOfWaypoints = waypoints.Count;
        CalculateTotalWaypointsTime();
    }

    private void CalculateTotalWaypointsTime()
    {
        if (!hasCalculateTheTime)
        {
            hasCalculateTheTime = true; 
            Transform firstWaypoint = waypoints[0];
            foreach (Transform child in waypointParent.transform)
            {
                totalDistance += Vector3.Distance(firstWaypoint.position, child.position);
                firstWaypoint = child;
            }
        }
    }

    private void BalloonProgres()
    {
        distanceTraveled = totalDistance / (Time.time - startTime);
        precentTraveled = totalDistance / distanceTraveled;
    }


    private void MoveEnemy()
    {
        if (enemySpawnedIn)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);      
        }
    }

    private void CheckEnemysPos()
    {
        if (enemySpawnedIn && !hasFinished)
        {
            if (amountOfWaypoints - 1 == waypointsPassed)
            {
                hasFinished = true;
            }

            //if the enemy is close to the waypoint then the currentwaypoint changes into the next waypoint in the list
            if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
            {
                currentWaypoint = waypoints[currentWaypoint.GetSiblingIndex() + 1];
                waypointsPassed++;
                transform.LookAt(currentWaypoint.position);
            }
        }
    }
    private void ResetStartPos()
    {
        //it place the enemy on the first waypoint
        transform.position = waypoints[0].position;
        currentWaypoint = waypoints[0];
    }

    private void FinishedWaypoints()
    {
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold && hasFinished)
        { 
            enemySpawnedIn = false;
            hasFinished = false;
            Debug.Log("Finished");
            enemySpawner.enemysLeft--;
            playerHealth.DecreaseHealth(decreasePlayerLife);
            gameObject.SetActive(false);
        }
    }

    public void ChangeBalloonSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
