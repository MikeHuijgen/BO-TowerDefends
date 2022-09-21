using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float damage;
    [SerializeField] private int poolCount;
    [SerializeField] private List<GameObject> projectilePool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject newProjectile = Instantiate(projectile, transform);
            newProjectile.SetActive(false);
            projectilePool.Add(newProjectile);
        }
    }




}
