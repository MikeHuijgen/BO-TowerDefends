using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickTowerImage : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    private GameObject tower;

    public void ClickedOnImage()
    {
        tower = Instantiate(towerPrefab, new Vector3(0,1,0), Quaternion.identity);
        tower.GetComponent<PlaceTower>().TowerSelected(true);
    }
}
