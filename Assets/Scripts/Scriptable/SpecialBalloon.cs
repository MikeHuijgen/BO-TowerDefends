using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Balloon", menuName = "Create special balloon")]
public class SpecialBalloon : ScriptableObject
{
    [Header("Balloon Settings")]
    public int BalloonHealth;
    [Range(1, 50)] public float BalloonSpeed;
    public Mesh balloonMesh;
}
