using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowWaypoint : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int decreasePlayerLife;
    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    private int amountOfWaypoints = 0;
    private int waypointsPassed = 0;
    private float distanceThreshold = 0.1f;

    public bool enemySpawnedIn = false;
    private bool hasFinished = false;

    private Transform currentWaypoint;
    private GameObject waypointParent;
    private EnemySpawner enemySpawner;
    private PlayerHealth playerHealth;

    private void OnEnable()
    {
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
    }
    private void ResetStartPos()
    {
        //it place the enemy on the first waypoint
        transform.position = waypoints[0].position;
        currentWaypoint = waypoints[0];
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
