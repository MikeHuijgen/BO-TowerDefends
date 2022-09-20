using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEnemyInRange : MonoBehaviour
{
    [Header("Target List")]
    [SerializeField] private List<Transform> enemys = new List<Transform>();
    private Tower tower;

    private void OnEnable()
    {
        enemys.Clear();
        tower = GetComponentInParent<Tower>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            tower.AddEnemy(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            tower.RemoveEnemy(other.transform);
        }
    }
}
