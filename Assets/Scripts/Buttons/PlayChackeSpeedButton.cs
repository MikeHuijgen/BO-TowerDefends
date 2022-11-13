using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayChackeSpeedButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject redArrowPartical;
    [SerializeField] private GameObject greenArrowPartical;
    private Waves wave;
    private bool isFast;
    private bool isNormale;
    private bool gameStarted;

    void Start()
    {
        wave = FindObjectOfType<Waves>();
        buttonText.text = "Play";
    }

    public void OnClickPlayer()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            isNormale = true;
            wave.PlayerStartedTheGame();
            redArrowPartical.SetActive(false);
            greenArrowPartical.SetActive(false);
        }

        if (isFast)
        {
            Time.timeScale = 3f;
            isFast = false;
            isNormale = true;
            buttonText.text = "Normale Speed";
        }
        else if (isNormale)
        {
            Time.timeScale = 1f;
            isNormale = false;
            isFast = true;
            buttonText.text = "Fast Speed";
        }
    }

}
