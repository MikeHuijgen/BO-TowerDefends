using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int lives;
    [SerializeField] private TMP_Text playerLifeText;
    [SerializeField] private GameObject deathScreen;

    private void Update()
    {
        ShowLifeInGame();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void ShowLifeInGame()
    {
        if (lives <= 0)
        {
            deathScreen.SetActive(true);
            Time.timeScale = 0;
        }
        playerLifeText.text = $"Lives {lives}";
    }

    public void DecreaseHealth(int amount)
    {
        lives -= amount;

    }
}
