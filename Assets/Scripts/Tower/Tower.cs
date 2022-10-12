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
    [SerializeField] private TowerType towerType;
    [SerializeField] private TargetStyle targetStyle;
    [SerializeField] private GameObject dartPrefab;
    [SerializeField] private List<EnemyFollowWaypoint> balloonList = new List<EnemyFollowWaypoint>();

    [Header("Tower Check")]
    [SerializeField] private int balloonsPoped;
    [SerializeField] private Transform currentTarget;
    
    public bool playerCanSelect = false;
    private float currentFireRate;

    private List<GameObject> dartPool = new List<GameObject>();

    Ray ray;

    private enum TowerType 
    { 
        normal,
        sniper
    }

    public enum TargetStyle
    {
        first,
        last
    }
        
    private void OnEnable()
    {
        playerCanSelect = true;
        currentFireRate = fireRate;
        towerRangeTransform.GetComponent<CheckForEnemyInRange>().enabled = true;
        towerRangeTransform.GetComponent<CapsuleCollider>().enabled = true;
        FillDartPool();
    }

    private void Update()
    {
        FindTarget();
        AttackEnemy();
        ResetTowerTarget();
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

    public void AddEnemyToTargetList(Transform balloon)
    {
        balloonList.Add(balloon.GetComponent<EnemyFollowWaypoint>());
    }

    public void RemoveEnemyFromTargetList(Transform enemy)
    {
        balloonList.Remove(enemy.GetComponent<EnemyFollowWaypoint>());
    }

    private void FindTarget()
    {
        if (balloonList.Count == 0) { return; }

        switch (targetStyle)
        {
            case TargetStyle.first:
                GetFirstTarget();
                break;
            case TargetStyle.last:
                GetLastTarget();
                break;
            default:
                Debug.Log("Tower has no target style");
                break;
        }
    }

    private void GetFirstTarget()
    {
        EnemyFollowWaypoint target = balloonList[0];
        for (int i = 0; i < balloonList.Count; i++)
        {
            if (balloonList[i].inGameTime > target.inGameTime)
            {
                target = balloonList[i];
            }
            currentTarget = target.transform;
        }
    }

    private void GetLastTarget()
    {
        EnemyFollowWaypoint target = balloonList[0];
        for (int i = 0; i < balloonList.Count; i++)
        {
            if (balloonList[i].inGameTime < target.inGameTime)
            {
                target = balloonList[i];
            }
            currentTarget = target.transform;
        }
    }

    private void AttackEnemy()
    {
        if (currentTarget == null) { return; }

        switch (towerType)
        {
            case TowerType.normal:
                AttackWithDarts();
                break;
            case TowerType.sniper:
                AttackWithRay();
                break;
            default:
                break;
        }

    }

    private void AttackWithDarts()
    {
        currentFireRate -= Time.deltaTime;

        foreach (var dart in dartPool)
        {
            if (!dart.activeInHierarchy && currentFireRate <= 0)
            {
                transform.LookAt(new Vector3(currentTarget.position.x, 1, currentTarget.position.z));
                dart.GetComponent<Dart>().SetUpDart(currentTarget.transform, transform, towerDamage);
                currentFireRate = fireRate;

            }
        }
    }

    private void AttackWithRay()
    {
        currentFireRate -= Time.deltaTime;

        ray.origin = transform.position;
        ray.direction = transform.forward;

        transform.LookAt(new Vector3(currentTarget.position.x, 1, currentTarget.position.z));

        if (Physics.Raycast(ray, out RaycastHit hitInfo) && currentFireRate <= 0)
        {
            if (hitInfo.transform == currentTarget.transform)
            {
                currentTarget.GetComponent<Enemy>().DecreaseHealth(towerDamage, this.transform);
                currentFireRate = fireRate;
            }
        }
    }

    private void ResetTowerTarget()
    {
        if (balloonList.Count == 0) 
        {
            currentTarget = null;
        }
    }

    public void EnemyGotKilledByTower(Transform enemy)
    {
        if (!balloonList.Contains(enemy.GetComponent<EnemyFollowWaypoint>())) { return; }
        balloonList.Remove(enemy.GetComponent<EnemyFollowWaypoint>());
    }

    public void towerGotSelected()
    {
        towerRangeTransform.GetComponent<MeshRenderer>().enabled = true;
    }
}
