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
    [SerializeField] private List<Transform> enemys = new List<Transform>();

    [Header("Attack style")]
    [SerializeField] private bool first;
    [SerializeField] private bool last;

    private Transform target;
    private TowerAttack towerAttack;

    private void OnEnable()
    {
        towerAttack = GetComponent<TowerAttack>();
        towerRangeTransform.GetComponent<CheckForEnemyInRange>().enabled = true;
        towerRangeTransform.GetComponent<CapsuleCollider>().enabled = true;
        Debug.Log("Hey");
    }

    private void Update()
    {
        LookAt();
    }

    private void LookAt()
    {
        if (enemys.Count > 0)
        {
            transform.LookAt(enemys[0]);
            towerAttack.AttackEnemy(enemys[0]);
        }
    }

    public void AddEnemy(Transform enemy)
    {
        enemys.Add(enemy);
    }

    public void RemoveEnemy(Transform enemy)
    {
        enemys.Remove(enemy);
    }
    
}
