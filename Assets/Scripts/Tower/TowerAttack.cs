using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private List<Transform> enemys = new List<Transform>();

    [SerializeField] private float currentAttackSpeed;
    private Tower tower;
    private bool canShoot = false;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        tower = GetComponent<Tower>();
        currentAttackSpeed = attackSpeed;
    }



    private void Update()
    {
        LookAt();
        HitEnemy();
        AttackSpeed();
    }

    private void LookAt()
    {
        if (enemys.Count > 0)
        {
            transform.LookAt(enemys[0]);
        }
    }

    private void HitEnemy()
    {
        if (enemys.Count == 0) 
        {
            return; 
        }
        else if (enemys.Count > 0 && canShoot)
        {
            ray.origin = transform.position;
            ray.direction = transform.forward;

            if (Physics.Raycast(ray, out hit, tower.towerRange))
            {
                hit.transform.GetComponent<Enemy>().DecreaseHealth(tower.towerDamage);
                RemoveEnemy(hit.transform);
                currentAttackSpeed = attackSpeed;
            }
        }
    }

    private void AttackSpeed()
    {
        if (currentAttackSpeed <= 0)
        {
            currentAttackSpeed = 0;
            canShoot = true;
        }
        else
        {
            canShoot = false;
            currentAttackSpeed -= Time.deltaTime;
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
