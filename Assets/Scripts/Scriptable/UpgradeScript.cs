using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Create New Upgrade")]
public class UpgradeScript : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public int upgradeCost;
    public List<UpgradeSlot> UpgradeValueInfo = new List<UpgradeSlot>();
    
    [System.Serializable]
    public class UpgradeSlot
    {
        public UpgradeType upgradeType;
        public float upgradeValue;
    }   
    
}

public enum UpgradeType
{
    Range,
    Damage,
    AttackSpeed,
    CanSeeCamo
}

