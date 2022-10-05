using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] public int towerDamage;
    [SerializeField] [Range(5, 60)] public float towerRange;
    [SerializeField] [Range(0, 1)] public float rangeOpacity;
    public GameObject towerRangeTransform;
    [SerializeField] private float fireRate;
    [SerializeField] private int dartPoolAmount;

    [Header("Attack style")]
    [SerializeField] private bool targetFirst;
    [SerializeField] private bool targetLast;
    [SerializeField] private GameObject dartPrefab;
    [SerializeField] private List<EnemyFollowWaypoint> targetList = new List<EnemyFollowWaypoint>();

    [Header("Tower Check")]
    [SerializeField] private int balloonsPoped;

    private float lastCurrentTimeEnemy = 0f;
    [SerializeField] private float dartSpeed;
    private Transform towerTarget;
    private float currentFireRate;

    private List<GameObject> dartPool = new List<GameObject>();

    Ray ray;
    RaycastHit hit;

    private void OnEnable()
    {
        currentFireRate = fireRate;
        towerRangeTransform.GetComponent<CheckForEnemyInRange>().enabled = true;
        towerRangeTransform.GetComponent<CapsuleCollider>().enabled = true;
        FillDartPool();
    }

    private void Update()
    {
        LookAtTarget();
        AttackEnemy();
    }

    private void FillDartPool()
    {
        for (int i = 0; i < dartPoolAmount; i++)
        {
            GameObject newDart = Instantiate(dartPrefab, transform.position, Quaternion.identity);
            dartPool.Add(newDart);
            newDart.SetActive(false);
        }
    }

    public void AddEnemyToTargetList(Transform enemy)
    {
        targetList.Add(enemy.GetComponent<EnemyFollowWaypoint>());
    }

    public void RemoveEnemyFromTargetList(Transform enemy)
    {
        targetList.Remove(enemy.GetComponent<EnemyFollowWaypoint>());
    }

    private void LookAtTarget()
    {
        if (targetList.Count == 0) { return; }

        if (targetFirst)
        {
            EnemyFollowWaypoint target = targetList[0];

            for (int i = 1; i < targetList.Count; i++)
            {
                target = targetList[i].GetFirstBalloon(target, targetList[i]);
                towerTarget = target.transform;
                Vector3 dir = towerTarget.position - transform.position;
                Quaternion lookRoation = Quaternion.LookRotation(dir);
                Vector3 rotation = lookRoation.eulerAngles;
                transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            }
        }
    }

    private void AttackEnemy()
    {
        if (targetList.Count == 0) { return; }

        currentFireRate -= Time.deltaTime;

        foreach (var dart in dartPool)
        {
            if (!dart.activeInHierarchy && currentFireRate <= 0)
            {
                dart.GetComponent<Dart>().SetUpDart(towerTarget, transform, towerDamage);
                currentFireRate = fireRate;
                return;
            }
        }
    }

    public void EnemyGotKilledByTower(Transform enemy)
    {
        if (!targetList.Contains(enemy.GetComponent<EnemyFollowWaypoint>())) { return; }
        targetList.Remove(enemy.GetComponent<EnemyFollowWaypoint>());
    }
}
