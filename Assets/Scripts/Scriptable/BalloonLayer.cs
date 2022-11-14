using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Balloon", menuName = "Create Balloon Layer")]
public class BalloonLayer : ScriptableObject
{
    [Header("Color Settings")]
    public Color balloonColor;
    public Material balloonMaterial;
    public Mesh balloonMesh;
    [Header("Balloon Settings")]
    public int BalloonHealth;
    [Range(1, 50)] public float BalloonSpeed;
    public Vector3 balloonScale;
    public bool MOABBalloon;
    public bool camoBalloon;
}
