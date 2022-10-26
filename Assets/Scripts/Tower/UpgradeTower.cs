using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    public List<UpgradeScript> Path1 = new List<UpgradeScript>();
    public List<UpgradeScript> Path2 = new List<UpgradeScript>();

    private int upgradesDonePath1;
    private int upgradesDonePath2;
    public int path1Index;
    public int path2Index;

    private Tower tower;
    private Bank bank;

    private void Start()
    {
        bank = GameObject.FindObjectOfType<Bank>();
        tower = GetComponent<Tower>();
    }

    private void Update()
    {
        //is voor debugging
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpgradePath1();
        }
    }

    public void UpgradePath1()
    {
        if (upgradesDonePath1 >= Path1.Count) { return; }

        UpgradeScript upgradeScript = Path1[path1Index];
        UpgradeType upgradeType;
        float upgradeValue;
        int upgradeCost;
        for (int i = 0; i < upgradeScript.UpgradeValueInfo.Count; i++)
        {
            upgradeCost = upgradeScript.upgradeCost;
            bank.DecreaseBankAmount(upgradeScript.upgradeCost);
            upgradeType = upgradeScript.UpgradeValueInfo[i].upgradeType;
            upgradeValue = upgradeScript.UpgradeValueInfo[i].upgradeValue;
            TellTowerToUpgrade(upgradeType, upgradeValue, upgradeCost);
        }

        upgradesDonePath1++;
        path1Index++;
    }

    public void UpgradePath2()
    {
        if (upgradesDonePath2 >= Path2.Count) { return; }

        UpgradeScript upgradeScript = Path2[path2Index];
        UpgradeType upgradeType;
        float upgradeValue;
        int upgradeCost;
        for (int i = 0; i < upgradeScript.UpgradeValueInfo.Count; i++)
        {
            upgradeCost = upgradeScript.upgradeCost;
            bank.DecreaseBankAmount(upgradeScript.upgradeCost);
            upgradeType = upgradeScript.UpgradeValueInfo[i].upgradeType;
            upgradeValue = upgradeScript.UpgradeValueInfo[i].upgradeValue;
            TellTowerToUpgrade(upgradeType, upgradeValue, upgradeCost);
        }

        upgradesDonePath2++;
        path2Index++;
    }

    public void TellTowerToUpgrade(UpgradeType type, float value, int cost)
    {
        tower.HasBeenUpgraded(type, value, cost);
    }
}
