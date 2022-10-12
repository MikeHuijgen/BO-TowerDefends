using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerSelect : MonoBehaviour
{
    [SerializeField] private LayerMask towerLayer;

    private bool towerHasBeenSelected = false;  

    // dit moet nog verbeterd worden
    private void Update()
    {
        CheckIfPlayerClickOnTower();
    }

    private void CheckIfPlayerClickOnTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, towerLayer) && hitInfo.transform.GetComponent<Tower>().playerCanSelect)
            {
                hitInfo.transform.GetComponent<Tower>().towerGotSelected();
                towerHasBeenSelected = true;
            }
        }
    }
}
