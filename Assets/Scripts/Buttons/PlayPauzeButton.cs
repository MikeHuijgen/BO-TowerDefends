using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayPauzeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject redArrowPartical;
    [SerializeField] private GameObject greenArrowPartical;
    private Waves wave;
    private bool isPauze;
    private bool isPlaying;
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
            isPlaying = true;
            wave.PlayerStartedTheGame();
            redArrowPartical.SetActive(false);
            greenArrowPartical.SetActive(false);
        }

        if (isPauze)
        {
            Time.timeScale = 1.5f;
            isPauze = false;
            isPlaying = true;
            buttonText.text = "Play";
        }
        else if (isPlaying)
        {
            Time.timeScale = 1f;
            isPlaying = false;
            isPauze = true;
            buttonText.text = "Pauze";
        }
    }

}
