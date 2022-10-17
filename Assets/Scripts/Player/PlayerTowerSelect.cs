using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTowerSelect : MonoBehaviour
{
    [SerializeField] private LayerMask towerLayer;

    private bool towerHasBeenSelected = false;
    private ShowTowerInfo towerInfoPanel;
    private Transform selectedTower;

    private void Start()
    {
        towerInfoPanel = FindObjectOfType<ShowTowerInfo>();
        towerInfoPanel.gameObject.SetActive(false);
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
                selectedTower.GetComponent<Tower>().TowerGotDeselected();
                selectedTower = hit.transform;
                selectedTower.GetComponent<Tower>().TowerGotSelected();
               // towerInfoPanel.gameObject.SetActive(true);
               // towerInfoPanel.GetTowerScript(selectedTower.GetComponent<Tower>());
            }

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, towerLayer) && hitInfo.transform.GetComponent<Tower>().playerCanSelect && !towerHasBeenSelected)
            {
                hitInfo.transform.GetComponent<Tower>().TowerGotSelected();
                selectedTower = hitInfo.transform;
                towerHasBeenSelected = true;
                //towerInfoPanel.gameObject.SetActive(true);
                //towerInfoPanel.GetTowerScript(selectedTower.GetComponent<Tower>());
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
                if (Physics.Raycast(ray, Mathf.Infinity, towerLayer)) { return; }

                selectedTower.GetComponent<Tower>().TowerGotDeselected();
                towerHasBeenSelected = false;
                towerInfoPanel.gameObject.SetActive(false);
            }
        }
    }
}
