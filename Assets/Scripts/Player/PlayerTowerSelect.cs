using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerTowerSelect : MonoBehaviour
{
    [SerializeField] private LayerMask towerLayer;

    private bool towerHasBeenSelected = false;
    private ShowTowerInfo towerInfo;
    [SerializeField] private Transform selectedTower;

    private void Start()
    {
        towerInfo = FindObjectOfType<ShowTowerInfo>();
    }

    // dit moet nog verbeterd worden
    private void Update()
    {
        CheckIfPlayerClickOnTower();
        CheckIfPlayerClickNotTower();
    }

    private void CheckIfPlayerClickOnTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, towerLayer) && hit.transform != selectedTower && towerHasBeenSelected)
            {
                if (selectedTower != null)
                {
                    selectedTower.GetComponent<Tower>().TowerGotDeselected();
                }
                selectedTower = hit.transform;
                selectedTower.GetComponent<Tower>().TowerGotSelected();

                towerInfo.GetTowerInfo(selectedTower.GetComponent<Tower>(), selectedTower.GetComponentInChildren<SaveTowerPosition>(), selectedTower.GetComponent<UpgradeTower>());
            }

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, towerLayer) && hitInfo.transform.GetComponent<Tower>().playerCanSelect && !towerHasBeenSelected && !hitInfo.transform.GetComponent<PlaceTower>().isSelected)
            {
                hitInfo.transform.GetComponent<Tower>().TowerGotSelected();
                selectedTower = hitInfo.transform;
                towerHasBeenSelected = true;

                towerInfo.GetTowerInfo(selectedTower.GetComponent<Tower>(), selectedTower.GetComponentInChildren<SaveTowerPosition>(), selectedTower.GetComponent<UpgradeTower>());
            }
        }
    }

    private void CheckIfPlayerClickNotTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (towerHasBeenSelected && !selectedTower.GetComponent<Tower>().playerCanSelect)
            {
                if (Physics.Raycast(ray, Mathf.Infinity, towerLayer) || EventSystem.current.IsPointerOverGameObject()) { return; }

                selectedTower.GetComponent<Tower>().TowerGotDeselected();
                towerHasBeenSelected = false;
                towerInfo.TowerGotDeselected();
                SetSelectedTowerNull ();
            }
        }
    }

    public void SetSelectedTowerNull()
    {
        towerHasBeenSelected = false;
        selectedTower = null;
    }
}
