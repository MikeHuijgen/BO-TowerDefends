using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    private UpgradeTower upgradeTower;

    public void GetTower(UpgradeTower _upgradeTower)
    {
        upgradeTower = _upgradeTower;
    }

    public void OnClickUpgradePath1()
    {
        upgradeTower.UpgradePath1();
    }

    public void OnClickUpgradePath2()
    {
        upgradeTower.UpgradePath2();
    }
}
