using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] public float towerDamage;
    [SerializeField] [Range(5, 30)] public float towerRange;
    [SerializeField] [Range(0, 1)] public float rangeOpacity;
    public GameObject towerRangeTransform;
    [SerializeField] private float fireRate;

    [Header("Attack style")]
    [SerializeField] private bool targetFirst;
    [SerializeField] private bool targetLast;
    [SerializeField] private List<Transform> targetList = new List<Transform>();

    private float lastCurrentTimeEnemy = 0f;
    private Transform myTarget;
    private float currentFireRate;

    Ray ray;
    RaycastHit hit;

    private void OnEnable()
    {
        currentFireRate = fireRate;
        towerRangeTransform.GetComponent<CheckForEnemyInRange>().enabled = true;
        towerRangeTransform.GetComponent<CapsuleCollider>().enabled = true;
    }

    private void Update()
    {
        LookAtTarget();
    }

    public void AddEnemyToTargetList(Transform enemy)
    {
        targetList.Add(enemy);
    }

    public void RemoveEnemyFromTargetList(Transform enemy)
    {
        targetList.Remove(enemy);
    }

    private void LookAtTarget()
    {
        if (targetList.Count == 0) { return; }

        lastCurrentTimeEnemy = 0;

        for (int i = 0; i < targetList.Count; i++) 
        { 
            if (lastCurrentTimeEnemy < targetList[i].GetComponent<Enemy>().inGameTime && targetFirst)
            {
                lastCurrentTimeEnemy = targetList[i].GetComponent<Enemy>().inGameTime;
                transform.LookAt(targetList[i]);
                myTarget = targetList[i];
            }
        }

        AttackEnemy();
    }

    private void AttackEnemy()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        currentFireRate -= Time.deltaTime;

        if (Physics.Raycast(ray,out hit, towerRange) && currentFireRate <= 0)
        {
            currentFireRate = fireRate;
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                transform.parent.BroadcastMessage("EnemyGotKilledByTower", enemy.transform);
                enemy.DecreaseHealth(towerDamage);
            }
        }
    }

    public void EnemyGotKilledByTower(Transform enemy)
    {
        if (!targetList.Contains(enemy)) { return; }
        targetList.Remove(enemy);
    }
}
