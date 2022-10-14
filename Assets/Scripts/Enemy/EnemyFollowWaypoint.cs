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

    public float inGameTime = 0;
    private int amountOfWaypoints = 0;
    public int waypointsPassed = 0;
    private float distanceThreshold = 0.1f;

    public bool enemySpawnedIn = false;
    private bool hasFinished = false;
    private bool hasCalculateTheTime = false;

    public Transform currentWaypoint;
    private GameObject waypointParent;
    private EnemySpawner enemySpawner;
    private PlayerHealth playerHealth;


    private float totalDistance;
    private int waypointIndex;
    private float totalNeedsToTraveled;
    public float totalDistanceTraveled;
    private float totalDistanceNeedToTraveled;
    private float distanceTillNextWaypoint;


    private void OnEnable()
    {
        inGameTime = 0;
        totalDistanceNeedToTraveled = 0;
        totalDistance = 0;
        waypointIndex = 0;
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
        balloonProgressOnCurWaypoint();
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
        CalculateTotalDistance();
    }

    private void CalculateTotalDistance()
    {
        //Calculate how far the total distance is
        Transform waypoint = waypoints[0];
        foreach (Transform child in waypointParent.transform)
        {
            totalDistance += Vector3.Distance(waypoint.position, child.position);
            waypoint = child;
        }
    }

    private void balloonProgressOnCurWaypoint()
    {
        //Checks how far the balloon is on the total distance
        float newDistance = Vector3.Distance(transform.position, currentWaypoint.position);

        if (newDistance < distanceTillNextWaypoint)
        {
            float difference = distanceTillNextWaypoint - newDistance;
            totalNeedsToTraveled = totalDistanceNeedToTraveled - difference;
            totalDistanceTraveled = totalDistance - totalNeedsToTraveled;
        }      
    }

    private void BalloonProgres()
    {
        //Checks everytime the balloon change waypoints how long the distance is to the end from its current waypoint
        distanceTillNextWaypoint = Vector3.Distance(waypoints[waypointIndex].position, currentWaypoint.position);
        totalDistanceNeedToTraveled = 0;
        Transform waypoint = waypoints[waypointIndex];
 
        for (int i = waypointIndex; i < waypoints.Count; i++)
        {
            totalDistanceNeedToTraveled += Vector3.Distance(waypoint.position, waypoints[i].position);
            waypoint = waypoints[i];
        }
        waypointIndex++;
    }

    private void MoveEnemy()
    {
        if (enemySpawnedIn)
        {
            //inGameTime += Time.deltaTime * moveSpeed;
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
                BalloonProgres();
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
