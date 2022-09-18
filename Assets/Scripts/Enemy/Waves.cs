using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private int wave;

    private void GoToNextWave()
    {
        wave++;
    }
}
