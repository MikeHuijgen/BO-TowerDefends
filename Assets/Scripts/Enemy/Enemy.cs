using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject pathParent;
    [SerializeField] private List<Transform> path = new List<Transform>();

    private NavMeshAgent agent;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowWaypoints());
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private void FindPath()
    {
        //find the path for the enemy
        path.Clear();

        pathParent = GameObject.FindGameObjectWithTag("waypointParent");

        foreach (Transform child in pathParent.transform)
        {
            path.Add(child);
        }
    }

    IEnumerator FollowWaypoints()
    {
        //follow path for path till the enemy is on the end
        foreach (Transform waypointPos in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypointPos.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += moveSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForFixedUpdate();
            }

        }

        FinishedPath();
    }

    private void FinishedPath()
    {
        Debug.Log("Finished");
        gameObject.SetActive(false);
    }
}
