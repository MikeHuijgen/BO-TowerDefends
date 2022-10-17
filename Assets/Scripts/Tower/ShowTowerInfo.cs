using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTowerInfo : MonoBehaviour
{
    public void GetTowerScript(Tower tower)
    {
        Debug.Log(tower.towerRange);
    }
}
