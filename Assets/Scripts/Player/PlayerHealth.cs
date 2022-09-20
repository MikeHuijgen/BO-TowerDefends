using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int lives;
    [SerializeField] private TMP_Text playerLifeText;

    public void DecreaseHealth(int amount)
    {
        lives -= amount;

    }
}
