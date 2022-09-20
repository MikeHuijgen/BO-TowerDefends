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

    [Header("Target List")]
    [SerializeField] private List<Transform> targets = new List<Transform>();

    private void OnEnable()
    {
        targets.Clear();
        towerRangeTransform.GetComponent<CheckForEnemyInRange>().enabled = true;
        Debug.Log("Hey");
    }

    private void Update()
    {
        LookAt();
    }

    private void LookAt()
    { 
        if (targets.Count > 0)
        {
            transform.LookAt(targets[0]);
        }
    }

    public void AddEnemyInRange(Transform enemy)
    {
        targets.Add(enemy);
    }

    public void RemoveEnemyInRange(Transform enemy)
    {
        targets.Remove(enemy);
    }
}
