using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTowerInfo : MonoBehaviour
{
    [SerializeField] private GameObject leftPanel;
    [SerializeField] private GameObject rightPanel;
    [Header("RightPanel")]
    [Header("Upgrade 1")]
    [SerializeField] private TMP_Text rightUpgradeName1;
    [SerializeField] private TMP_Text rightUpgradeDescription1;
    [SerializeField] private TMP_Text rightUpgradeCost1;
    [Header("Upgrade 2")]
    [SerializeField] private TMP_Text rightUpgradeName2;
    [SerializeField] private TMP_Text rightUpgradeDescription2;
    [SerializeField] private TMP_Text rightUpgradeCost2;
    [Header("leftPanel")]
    [Header("Upgrade 1")]
    [SerializeField] private TMP_Text leftUpgradeName1;
    [SerializeField] private TMP_Text leftUpgradeDescription1;
    [SerializeField] private TMP_Text leftUpgradeCost1;
    [Header("Upgrade 2")]
    [SerializeField] private TMP_Text leftUpgradeName2;
    [SerializeField] private TMP_Text leftUpgradeDescription2;
    [SerializeField] private TMP_Text leftUpgradeCost2;

    private Tower _selectedTower;
    private UpgradeTower _upgradeTower;
    private SaveTowerPosition _saveTowerPosition;
    private PlayerTowerSelect playerTowerSelect;
    public bool towerInfoShow;

    private void Start()
    {
        playerTowerSelect = GameObject.FindObjectOfType<PlayerTowerSelect>();
        leftPanel.SetActive(false);
        rightPanel.SetActive(false);
    }

    private void Update()
    {
        ShowingTowerInfo();
    }

    public void GetTowerInfo(Tower tower, SaveTowerPosition towerPos, UpgradeTower upgradeTower)
    {
        _saveTowerPosition = towerPos;
        _upgradeTower = upgradeTower;
        if (towerPos.posIsLeft)
        {
            _selectedTower = tower;
            towerInfoShow = true;
            rightPanel.SetActive(true);
            leftPanel.SetActive(false);
        }
        else if (towerPos.posIsRight)
        {
            _selectedTower = tower;
            towerInfoShow = true;
            leftPanel.SetActive(true);
            rightPanel.SetActive(false);
        }
    }

    public void TowerGotDeselected()
    {
        playerTowerSelect.SetSelectedTowerNull();
        towerInfoShow = false;
        leftPanel.SetActive(false);
        rightPanel.SetActive(false);
    }

    private void ShowingTowerInfo()
    {
        if (_saveTowerPosition == null) { return; }
        if (_saveTowerPosition.posIsRight && towerInfoShow)
        {
            FillLeftInfo();
        }
        else if (_saveTowerPosition.posIsLeft && towerInfoShow)
        {
            FillRightInfo();
        }
    }

    private void FillRightInfo()
    {
        // show the info from the upgrades on the right panel
        if (_upgradeTower.path1Index < _upgradeTower.Path1.Count)
        {
            rightPanel.BroadcastMessage("GetTower", _upgradeTower);
            rightPanel.BroadcastMessage("GetTowerTransform", _upgradeTower.transform);

            rightUpgradeName1.text = _upgradeTower.Path1[_upgradeTower.path1Index].upgradeName;
            rightUpgradeDescription1.text = _upgradeTower.Path1[_upgradeTower.path1Index].description;
            rightUpgradeCost1.text = $"Cost : {_upgradeTower.Path1[_upgradeTower.path1Index].upgradeCost}";
        }
        if ( _upgradeTower.path2Index < _upgradeTower.Path2.Count)
        {
            rightPanel.BroadcastMessage("GetTower", _upgradeTower);
            rightPanel.BroadcastMessage("GetTowerTransform", _upgradeTower.transform);

            rightUpgradeName2.text = _upgradeTower.Path2[_upgradeTower.path2Index].upgradeName;
            rightUpgradeDescription2.text = _upgradeTower.Path2[_upgradeTower.path2Index].description;
            rightUpgradeCost2.text = $"Cost : {_upgradeTower.Path2[_upgradeTower.path2Index].upgradeCost}";
        }
    }

    private void FillLeftInfo()
    {
        // show the info from the upgrades on the left panel
        if (_upgradeTower.path1Index < _upgradeTower.Path1.Count)
        {
            leftPanel.BroadcastMessage("GetTower", _upgradeTower);
            leftPanel.BroadcastMessage("GetTowerTransform", _upgradeTower.transform);

            leftUpgradeName1.text = _upgradeTower.Path1[_upgradeTower.path1Index].upgradeName;
            leftUpgradeDescription1.text = _upgradeTower.Path1[_upgradeTower.path1Index].description;
            leftUpgradeCost1.text = $"Cost : {_upgradeTower.Path1[_upgradeTower.path1Index].upgradeCost}";
        }
        if (_upgradeTower.path2Index < _upgradeTower.Path2.Count)
        {
            leftPanel.BroadcastMessage("GetTower", _upgradeTower);
            leftPanel.BroadcastMessage("GetTowerTransform", _upgradeTower.transform);

            leftUpgradeName2.text = _upgradeTower.Path2[_upgradeTower.path2Index].upgradeName;
            leftUpgradeDescription2.text = _upgradeTower.Path2[_upgradeTower.path2Index].description;
            leftUpgradeCost2.text = $"Cost : {_upgradeTower.Path2[_upgradeTower.path2Index].upgradeCost}";
        }
    }
}
