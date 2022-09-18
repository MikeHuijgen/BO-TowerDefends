using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour
{
    public void TowerHasBeenSelected()
    {
        BroadcastMessage("DisableTowerImage");
    }

    public void TowerHasBeenPlaced()
    {
        BroadcastMessage("EnableTowerImage");
    }
}
