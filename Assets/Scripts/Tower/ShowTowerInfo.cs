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
    [SerializeField] private TMP_Text rightLastUpgradeText1;
    [Header("Upgrade 2")]
    [SerializeField] private TMP_Text rightUpgradeName2;
    [SerializeField] private TMP_Text rightUpgradeDescription2;
    [SerializeField] private TMP_Text rightUpgradeCost2;
    [SerializeField] private TMP_Text rightLastUpgradeText2;
    [Header("leftPanel")]
    [Header("Upgrade 1")]
    [SerializeField] private TMP_Text leftUpgradeName1;
    [SerializeField] private TMP_Text leftUpgradeDescription1;
    [SerializeField] private TMP_Text leftUpgradeCost1;
    [SerializeField] private TMP_Text leftLastUpgradeText1;
    [Header("Upgrade 2")]
    [SerializeField] private TMP_Text leftUpgradeName2;
    [SerializeField] private TMP_Text leftUpgradeDescription2;
    [SerializeField] private TMP_Text leftUpgradeCost2;
    [SerializeField] private TMP_Text leftLastUpgradeText2;

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
        CheckIfPathIsLastUpgrade();
        PathMaxedOut();
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
            rightPanel.BroadcastMessage("GetTower", _upgradeTower);
            rightPanel.BroadcastMessage("GetTowerTransform", _upgradeTower.transform);
            rightPanel.BroadcastMessage("GetTowerGameobject", _selectedTower);
        }
        else if (towerPos.posIsRight)
        {
            _selectedTower = tower;
            towerInfoShow = true;
            leftPanel.SetActive(true);
            rightPanel.SetActive(false);
            leftPanel.BroadcastMessage("GetTower", _upgradeTower);
            leftPanel.BroadcastMessage("GetTowerTransform", _upgradeTower.transform);
            leftPanel.BroadcastMessage("GetTowerGameobject", _selectedTower);
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
        if (towerInfoShow)
        {
            FillTowerInfo();
        }
    }

    private void FillTowerInfo()
    {
        // show the info from the upgrades on the right panel
        if (_upgradeTower.path1Index < _upgradeTower.Path1.Count)
        {
            rightUpgradeName1.text = _upgradeTower.Path1[_upgradeTower.path1Index].upgradeName;
            rightUpgradeDescription1.text = _upgradeTower.Path1[_upgradeTower.path1Index].description;
            rightUpgradeCost1.text = $"Cost : {_upgradeTower.Path1[_upgradeTower.path1Index].upgradeCost}";

            leftUpgradeName1.text = _upgradeTower.Path1[_upgradeTower.path1Index].upgradeName;
            leftUpgradeDescription1.text = _upgradeTower.Path1[_upgradeTower.path1Index].description;
            leftUpgradeCost1.text = $"Cost : {_upgradeTower.Path1[_upgradeTower.path1Index].upgradeCost}";
        }
        if ( _upgradeTower.path2Index < _upgradeTower.Path2.Count)
        {
            rightUpgradeName2.text = _upgradeTower.Path2[_upgradeTower.path2Index].upgradeName;
            rightUpgradeDescription2.text = _upgradeTower.Path2[_upgradeTower.path2Index].description;
            rightUpgradeCost2.text = $"Cost : {_upgradeTower.Path2[_upgradeTower.path2Index].upgradeCost}";

            leftUpgradeName2.text = _upgradeTower.Path2[_upgradeTower.path2Index].upgradeName;
            leftUpgradeDescription2.text = _upgradeTower.Path2[_upgradeTower.path2Index].description;
            leftUpgradeCost2.text = $"Cost : {_upgradeTower.Path2[_upgradeTower.path2Index].upgradeCost}";
        }
        if (_upgradeTower.path1Done && !_upgradeTower.path2Max)
        {
            rightUpgradeName1.text = "";
            rightUpgradeDescription1.text = "Max Upgrade";
            rightUpgradeCost1.text = "";

            leftUpgradeName1.text = "";
            leftUpgradeDescription1.text = "Max Upgrade";
            leftUpgradeCost1.text = "";
        }
        if (_upgradeTower.path2Done && !_upgradeTower.path1Max)
        {
            rightUpgradeName2.text = "";
            rightUpgradeDescription2.text = "Max Upgrade";
            rightUpgradeCost2.text = "";

            leftUpgradeName2.text = "";
            leftUpgradeDescription2.text = "Max Upgrade";
            leftUpgradeCost2.text = "";
        }
    }

    private void CheckIfPathIsLastUpgrade()
    {
        if (_saveTowerPosition == null) { return; }
        if (_upgradeTower.path1Index == _upgradeTower.Path1.Count - 1)
        {
            rightLastUpgradeText1.text = "Last Upgrade in path";
            leftLastUpgradeText1.text = "Last Upgrade in path";
        }
        if (_upgradeTower.path2Index == _upgradeTower.Path2.Count - 1)
        {
            rightLastUpgradeText2.text = "Last Upgrade in path";
            leftLastUpgradeText2.text = "Last Upgrade in path";
        }

        if (_upgradeTower.path1Index != _upgradeTower.Path1.Count - 1)
        {
            rightLastUpgradeText1.text = "";
            leftLastUpgradeText1.text = "";
        }
        if (_upgradeTower.path2Index != _upgradeTower.Path2.Count - 1)
        {
            rightLastUpgradeText2.text = "";
            leftLastUpgradeText2.text = "";
        }
    }

    private void PathMaxedOut()
    {
        if (_saveTowerPosition == null) { return; }
        if (_upgradeTower.path1Max && _upgradeTower.path2Done)
        {
            leftUpgradeName2.text = "";
            leftUpgradeDescription2.text = "Path Closed";
            leftUpgradeCost2.text = "";
            leftLastUpgradeText2.text = "";

            rightUpgradeName2.text = "";
            rightUpgradeDescription2.text = "Path Closed";
            rightUpgradeCost2.text = "";
            rightLastUpgradeText2.text = "";
        }
        if (_upgradeTower.path2Max && _upgradeTower.path1Done)
        {
            leftUpgradeName1.text = "";
            leftUpgradeDescription1.text = "Path Closed";
            leftUpgradeCost1.text = "";
            leftLastUpgradeText1.text = "";

            rightUpgradeName1.text = "";
            rightUpgradeDescription1.text = "Path Closed";
            rightUpgradeCost1.text = "";
            rightLastUpgradeText1.text = "";
        }
    }
}
