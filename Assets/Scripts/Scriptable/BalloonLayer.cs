using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Balloon", menuName = "Create Balloon Layer")]
public class BalloonLayer : ScriptableObject
{
    [Header("Color Settings")]
    public Color balloonColor;
    [Header("Balloon Settings")]
    public int BalloonHealth;
    [Range(1,50)] public float BalloonSpeed;
    public int balloonKey;
}
