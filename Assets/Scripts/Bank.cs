using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [Header("Bank Settings")]
    [SerializeField] public int bankBalance;
    [SerializeField] float timer;

    [SerializeField] private TMP_Text bankAmountText;

    private void Update()
    {
        CheckBankBalance();
        test();
    }

    private void test()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            bankBalance += 3;
            timer = 3;
        }
    }


    private void CheckBankBalance()
    {
        bankAmountText.text = bankBalance.ToString();
    }

    public void IncreaseBankAmount(int amount)
    {
        bankBalance += amount;
    }

    public void DecreaseBankAmount(int amount)
    {
        bankBalance -= amount;
    }
}
