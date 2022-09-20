using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyID : MonoBehaviour
{
    public int enemyID;

    public void GiveEnemyIDNumber(int idNumber)
    {
        enemyID = idNumber;
    }
}
