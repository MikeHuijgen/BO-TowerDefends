using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTowerInfo : MonoBehaviour
{
    [SerializeField] private GameObject leftPanel;
    [SerializeField] private GameObject rightPanel;

    private Tower selectedTower;

    private void Start()
    {
        leftPanel.SetActive(false);
        rightPanel.SetActive(false);
    }

    public void GetTowerInfo(Tower tower, SaveTowerPosition towerPos)
    {
        if (towerPos.posIsLeft)
        {
            selectedTower = tower;
            rightPanel.SetActive(true);
            leftPanel.SetActive(false);
        }
        else if (towerPos.posIsRight)
        {
            selectedTower = tower;
            leftPanel.SetActive(true);
            rightPanel.SetActive(false);
        }
    }

    public void TowerGotDeselected()
    {
        leftPanel.SetActive(false);
        rightPanel.SetActive(false);
    }
}
