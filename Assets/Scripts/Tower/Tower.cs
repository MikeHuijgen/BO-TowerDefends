using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] public int towerDamage;
    [SerializeField] [Range(5, 30)] public float towerRange;
    [SerializeField] [Range(0, 1)] public float rangeOpacity;
    [SerializeField] public GameObject towerRangeTransform;

    [Header("Attack style")]
    [SerializeField] private bool first;
    [SerializeField] private bool last;

    private TowerAttack towerAttack;

    private void OnEnable()
    {
        towerAttack = GetComponent<TowerAttack>();
        towerRangeTransform.GetComponent<CheckForEnemyInRange>().enabled = true;
        towerRangeTransform.GetComponent<CapsuleCollider>().enabled = true;
        Debug.Log("Hey");
    }    
}
