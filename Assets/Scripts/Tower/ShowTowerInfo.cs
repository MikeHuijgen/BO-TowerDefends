using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTowerInfo : MonoBehaviour
{
    public void GetTowerScript(Tower tower)
    {
        Debug.Log(tower.towerRange);
    }
}
