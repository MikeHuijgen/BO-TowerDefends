using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    public List<UpgradeScript> Path1 = new List<UpgradeScript>();
    public List<UpgradeScript> Path2 = new List<UpgradeScript>();

    public int path1Index;
    public int path2Index;

    public bool path1Done = false;
    public bool path2Done = false;
    public bool path1Max = false;
    public bool path2Max = false;

    private Tower tower;
    private Bank bank;

    private void Start()
    {
        bank = GameObject.FindObjectOfType<Bank>();
        tower = GetComponent<Tower>();
    }

    private void Update()
    {
        CheckIfAPathIsDone();
    }

    private void CheckIfAPathIsDone()
    {
        //check if one of the paths are done with upgrades
        if (path1Index >= Path1.Count && !path1Done)
        {
            path1Done = true;
        }
        if (path2Index >= Path2.Count && !path2Done)
        {
            path2Done = true;
        }

        if (path1Done && path2Index == Path2.Count - 1)
        {
            path2Done = true;
            path1Max = true;
        }
        if (path2Done && path1Index == Path1.Count - 1)
        {
            path1Done = true;
            path2Max = true;
        }
    }

    public void UpgradePath1()
    {
        if (!path1Done)
        {
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

            path1Index++;
        }
    }

    public void UpgradePath2()
    {
        if (!path2Done)
        {
            UpgradeScript upgradeScript = Path2[path2Index];
            UpgradeType upgradeType;
            float upgradeValue;
            int upgradeCost;
            for (int i = 0; i < upgradeScript.UpgradeValueInfo.Count; i++)
            {
                upgradeCost = upgradeScript.upgradeCost;
                upgradeType = upgradeScript.UpgradeValueInfo[i].upgradeType;
                upgradeValue = upgradeScript.UpgradeValueInfo[i].upgradeValue;
                TellTowerToUpgrade(upgradeType, upgradeValue, upgradeCost);
            }
            bank.DecreaseBankAmount(upgradeScript.upgradeCost);
            path2Index++;
        }
    }

    public void TellTowerToUpgrade(UpgradeType type, float value, int cost)
    {
        tower.HasBeenUpgraded(type, value, cost);
    }
}
