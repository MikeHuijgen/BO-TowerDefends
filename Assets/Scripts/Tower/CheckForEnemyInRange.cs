using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEnemyInRange : MonoBehaviour
{
    private Tower tower;

    private void OnEnable()
    {
        tower = GetComponentInParent<Tower>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            tower.AddEnemyInRange(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            tower.RemoveEnemyInRange(other.transform);
        }
    }
}
