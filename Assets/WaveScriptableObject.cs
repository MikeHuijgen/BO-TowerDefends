using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Scriptable Objects")]
public class WaveScriptableObject : ScriptableObject
{
    [SerializeField] public List<BalloonSlot> balloons = new List<BalloonSlot>();

    [System.Serializable]
    public class BalloonSlot
    {
        public GameObject balloon;
        public int amount;
    }
}