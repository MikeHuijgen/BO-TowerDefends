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

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void GetTower(UpgradeTower _upgradeTower)
    {
        upgradeTower = _upgradeTower;
    }

    private void Update()
    {
        checkBankBalance();
    }

    public void OnClickUpgradePath1()
    {
        if (bank.bankBalance >= upgradeTower.Path1[upgradeTower.path1Index].upgradeCost)
        {
            upgradeTower.UpgradePath1();
        }
    }

    public void OnClickUpgradePath2()
    {
        if (bank.bankBalance >= upgradeTower.Path2[upgradeTower.path2Index].upgradeCost)
        {
            upgradeTower.UpgradePath2();
        }
    }

    private void checkBankBalance()
    {
        if (bank.bankBalance < upgradeTower.Path1[upgradeTower.path1Index].upgradeCost && buttonPath1)
        {
            transform.GetComponent<Button>().interactable = false;
            transform.GetComponent<Image>().color = Color.red;
        }
        else if (bank.bankBalance < upgradeTower.Path2[upgradeTower.path2Index].upgradeCost && buttonPath2)
        {
            transform.GetComponent<Button>().interactable = false;
            transform.GetComponent<Image>().color = Color.red;
        }
        else
        {
            transform.GetComponent<Button>().interactable = true;
            transform.GetComponent<Image>().color = Color.yellow;
        }

    }
}
