using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    private TMP_Text counterText;
    private int enemyCounter;

    void Start()
    {
        counterText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        ShowEnemyAmount();
    }

    private void ShowEnemyAmount()
    {
        counterText.text = $"{enemyCounter} Enemies Untill next wave";
    }

    public void DecreaseEnemyCounter()
    {
        enemyCounter--;
    }

    public void GetAmountOfEnemys(int amount)
    {
        enemyCounter = amount;
    }
}
