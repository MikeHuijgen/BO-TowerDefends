using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private Transform enemyToAttack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private Transform raySpawner;

    [SerializeField] private float currentAttackSpeed;
    private Tower tower;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        tower = GetComponent<Tower>();
        currentAttackSpeed = attackSpeed;
    }

    public void AttackEnemy(Transform enmey)
    {
        enemyToAttack = enmey;
    }

    private void Update()
    {
        HitEnemy();
    }

    private void HitEnemy()
    {
        if (enemyToAttack == null) { return; }

        ray.origin = raySpawner.position;
        ray.direction = raySpawner.forward;

        currentAttackSpeed -= Time.deltaTime;

        if (Physics.Raycast(ray, out hit, tower.towerRange) && currentAttackSpeed <= 0)
        {
            Debug.Log(hit.transform.name);
            hit.transform.GetComponent<Enemy>().DecreaseHealth(tower.towerDamage);
            currentAttackSpeed = attackSpeed;
        }
    }
}
