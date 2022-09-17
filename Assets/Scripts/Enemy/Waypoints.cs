using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    private Transform waypointParent;


    // Start is called before the first frame update
    void Start()
    {
        waypointParent = transform;

        foreach (Transform child in waypointParent)
        {
            wayPoints.Add(child);
        }
    }
}
