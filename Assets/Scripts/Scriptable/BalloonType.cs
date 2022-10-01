using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Balloon", menuName = "Create Balloon")]
public class BalloonType : ScriptableObject
{
    [Header("Color Settings")]
    public Color balloonColor;
    [Header("Balloon Settings")]
    public int BalloonHealth;
    public float BalloonSpeed;
}
