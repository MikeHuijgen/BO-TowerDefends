using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Scriptable Objects")]
public class WaveScriptableObject : ScriptableObject
{
    [SerializeField] public List<BalloonSlot> balloons = new List<BalloonSlot>();
    [SerializeField][Range(0.1f,10)] public float timeBetweenSpawn;

    [System.Serializable]
    public class BalloonSlot
    {
        public BalloonLayer balloonLayer;
        public int amount;
    }
}
