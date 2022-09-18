using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject pathParent;
    [SerializeField] private List<Transform> path = new List<Transform>();

    private void OnEnable()
    {
        FindWaypoints();
        ReturnToStart();
        StartCoroutine(FollowWaypoints());
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private void FindWaypoints()
    {
        path.Clear();

        pathParent = GameObject.FindGameObjectWithTag("pathParent");

        foreach (Transform child in pathParent.transform)
        {
            path.Add(child);
        }
    }

    IEnumerator FollowWaypoints()
    {
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

        FinishedAllWaypoints();
    }

    private void FinishedAllWaypoints()
    {
        Debug.Log("Finished");
        gameObject.SetActive(false);
    }
}
