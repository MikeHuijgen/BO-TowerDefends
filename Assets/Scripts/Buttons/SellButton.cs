using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    private Bank bank;
    private Transform myTower;
    private int gold;
    private ShowTowerInfo showTowerInfo;

    private void Start()
    {
        showTowerInfo = FindObjectOfType<ShowTowerInfo>();
        bank = FindObjectOfType<Bank>();
    }

    public void GetTowerTransform(Transform tower)
    {
        myTower = tower;
    }

    public void SellTower()
    {
        gold = myTower.GetComponent<Tower>().towerValue;
        gold = gold / 4;
        gold = gold * 3;
        bank.IncreaseBankAmount(gold);
        myTower.GetComponent<Tower>().TowerGotDeselected();
        showTowerInfo.TowerGotDeselected();
        Destroy(myTower.gameObject);
    }
}
