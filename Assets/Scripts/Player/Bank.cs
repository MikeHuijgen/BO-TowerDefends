using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [Header("Bank Settings")]
    public int bankBalance;

    [SerializeField] private TMP_Text bankAmountText;

    private void Update()
    {
        CheckBankBalance();
    }


    private void CheckBankBalance()
    {
        bankAmountText.text = $"${bankBalance}";
    }

    public void IncreaseBankAmount(int amount)
    {
        bankBalance += amount;
    }

    public void DecreaseBankAmount(int amount)
    {
        Debug.Log(amount);
        bankBalance -= amount;
    }
}
