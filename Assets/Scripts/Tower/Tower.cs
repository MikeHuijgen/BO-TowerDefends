using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] public int towerDamage;
    [SerializeField] [Range(5, 100)] public float towerRangeSphere;
    [SerializeField] [Range(5, 100)] public float towerRangeColliderTrans;
    [SerializeField] [Range(0, 1)] public float rangeOpacity;
    public GameObject towerRangeTransform;
    public GameObject towerRangeCollider;
    [SerializeField] private float fireRate;
    [SerializeField] private int dartPoolAmount;

    [Header("Attack style")]
    [SerializeField] private TowerType towerType;
    [SerializeField] public TargetStyle targetStyle;
    [SerializeField] private GameObject dartPrefab;
    [SerializeField] private ParticleSystem sniperMuzzleEffect;
    [SerializeField] private bool canSeeCamo;

    [Header("Tower Check")]
    [SerializeField] private int balloonsPoped;
    [SerializeField] private Transform currentTarget;
    
    public bool playerCanSelect = false;
    private float currentFireRate;
    public int towerValue;

    [SerializeField] private List<EnemyFollowWaypoint> balloonList = new List<EnemyFollowWaypoint>();
    private List<GameObject> dartPool = new List<GameObject>();

    private Transform dartPoolTransform;

    ShowTowerInfo towerInfoPanel;

    Ray ray;

    private enum TowerType 
    { 
        Projectile,
        RayCast
    }

    public enum TargetStyle
    {
        first,
        last
    }
        
    private void OnEnable()
    {
        towerValue = 0;
        towerValue = transform.GetComponent<PlaceTower>().towerCost;
        playerCanSelect = true;
        towerRangeCollider.GetComponent<CheckForEnemyInRange>().enabled = true;
        towerRangeCollider.GetComponent<CapsuleCollider>().enabled = true;
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
        if (towerType != TowerType.Projectile) { return; }
        foreach (Transform child in transform)
        {
            if (child.CompareTag("dartPool1"))
            {
                dartPoolTransform = child;
            }
        }
        for (int i = 0; i < dartPoolAmount; i++)
        {
            GameObject newDart = Instantiate(dartPrefab, dartPoolTransform.position, Quaternion.identity);
            newDart.transform.parent = dartPoolTransform;
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
            if (balloonList[i].GetComponent<Enemy>().CamoBalloon && !canSeeCamo) { return; }
            if (balloonList[i].totalDistanceTraveled > target.totalDistanceTraveled)
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
            if (balloonList[i].totalDistanceTraveled < target.totalDistanceTraveled)
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
            case TowerType.Projectile:
                AttackWithDarts();
                break;
            case TowerType.RayCast:
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
                transform.LookAt(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z));
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

        transform.LookAt(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z));

        if (Physics.Raycast(ray, out RaycastHit hitInfo) && currentFireRate <= 0)
        {
            sniperMuzzleEffect.Play();
            Debug.DrawLine(transform.position, currentTarget.position, Color.red, 1f);
            currentTarget.GetComponent<Enemy>().DecreaseHealth(towerDamage, this.transform);
            currentFireRate = fireRate;
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

    public void TowerGotSelected()
    {
        towerRangeTransform.GetComponent<MeshRenderer>().enabled = true;
        playerCanSelect = false;
    }

    public void TowerGotDeselected()
    {
        towerRangeTransform.GetComponent<MeshRenderer>().enabled = false;
        playerCanSelect = true;
    }

    public void HasBeenUpgraded(UpgradeType type, float value, int cost)
    {
        UpgradeType upgradeType = type;
        switch (upgradeType)
        {
            case UpgradeType.Range:
                towerValue += cost;
                towerRangeColliderTrans = value;
                towerRangeSphere = value;
                towerRangeTransform.transform.localScale = new Vector3(towerRangeSphere, 0, towerRangeSphere);
                towerRangeCollider.transform.localScale = new Vector3(towerRangeColliderTrans, 0, towerRangeColliderTrans);
                break;
            case UpgradeType.Damage:
                towerValue += cost;
                towerDamage = (int)value;
                break;
            case UpgradeType.AttackSpeed:
                towerValue += cost;
                fireRate = value;
                break;
            case UpgradeType.CanSeeCamo:
                canSeeCamo = true;
                break;
            default:
                break;
        }
    }

    public void ChangeTargetStyle(TargetStyle style)
    {
        targetStyle = style;
    }

    public void CantSelectTower()
    {
        playerCanSelect = false;
    }

    public void CanSelectTower()
    {
        playerCanSelect = true;
    }
}
