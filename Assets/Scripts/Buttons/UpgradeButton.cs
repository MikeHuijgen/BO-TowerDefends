using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private bool buttonPath1;
    [SerializeField] private bool buttonPath2;
    private UpgradeTower upgradeTower;
    private Bank bank;
    private ShowTowerInfo showTowerInfo;

    private void Start()
    {
        showTowerInfo = FindObjectOfType<ShowTowerInfo>();
        bank = FindObjectOfType<Bank>();
    }

    public void GetTower(UpgradeTower _upgradeTower)
    {
        upgradeTower = _upgradeTower;
    }

    private void Update()
    {
        if (showTowerInfo.towerInfoShow)
        {
            checkBankBalance();
        }

        PathDone();
    }

    public void OnClickUpgradePath1()
    {
        if (bank.bankBalance >= upgradeTower.Path1[upgradeTower.path1Index].upgradeCost && !upgradeTower.path1Done)
        {
            upgradeTower.UpgradePath1();
        }
    }

    public void OnClickUpgradePath2()
    {
        if (bank.bankBalance >= upgradeTower.Path2[upgradeTower.path2Index].upgradeCost && !upgradeTower.path2Done)
        {
            upgradeTower.UpgradePath2();
        }
    }

    private void checkBankBalance()
    {
        if (upgradeTower.path1Index < upgradeTower.Path1.Count && bank.bankBalance < upgradeTower.Path1[upgradeTower.path1Index].upgradeCost && buttonPath1)
        {
            SetButtonDeactive();

        }
        if (upgradeTower.path1Index < upgradeTower.Path1.Count && bank.bankBalance >= upgradeTower.Path1[upgradeTower.path1Index].upgradeCost && buttonPath1)
        {
            SetButtonActive();
        }
        if (upgradeTower.path2Index < upgradeTower.Path2.Count && bank.bankBalance < upgradeTower.Path2[upgradeTower.path2Index].upgradeCost && buttonPath2)
        {
            SetButtonDeactive();
        }
        if (upgradeTower.path2Index < upgradeTower.Path2.Count && bank.bankBalance >= upgradeTower.Path2[upgradeTower.path2Index].upgradeCost && buttonPath2)
        {
            SetButtonActive();
        }
    }


    private void PathDone()
    {
        if (upgradeTower.path1Done)
        {
            SetButtonDeactive();
        }

        if (upgradeTower.path2Done)
        {
            SetButtonDeactive();
        }
    }
    private void SetButtonActive()
    {
        transform.GetComponent<Button>().interactable = true;
        transform.GetComponent<Image>().color = Color.yellow;
    }

    private void SetButtonDeactive()
    {
        transform.GetComponent<Button>().interactable = false;
        transform.GetComponent<Image>().color = Color.red;
    }
}
