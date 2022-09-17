using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] [Range(5, 30)] public float towerRange;
    [SerializeField] [Range(0, 1)] public float rangeOpacity;
    [SerializeField] public GameObject towerRangeTransform;

    private void OnEnable()
    {
        Debug.Log("Hey");
    }
}
