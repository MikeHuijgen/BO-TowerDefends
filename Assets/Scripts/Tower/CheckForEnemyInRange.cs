using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEnemyInRange : MonoBehaviour
{
    [Header("Target List")]
    private TowerAttack towerAttack;

    private void OnEnable()
    {
        towerAttack = GetComponentInParent<TowerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            towerAttack.AddEnemy(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            towerAttack.RemoveEnemy(other.transform);
        }
    }
}
