using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Material towerMat;
    private Vector3 mousePos;
    private Vector3 worldPos;
    private GameObject tower;
    private bool TowerSelect = false;
    private RaycastHit hit;

    public void ClickedOnImage()
    {
        mousePos = Input.mousePosition;
        tower = Instantiate(towerPrefab, mousePos, Quaternion.identity);
        TowerSelect = true;
    }

    private void Update()
    {
        TowerToMousePos();
        PlaceTheTower();
        ChangeObjectColor();
    }

    private void TowerToMousePos()
    {
        if (TowerSelect)
        {
            mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, groundLayer))
            {
                hit = hitInfo;
                worldPos = hitInfo.point;
            }

            tower.transform.position = worldPos;
        }
    }

    private void PlaceTheTower()
    {
        if (Input.GetMouseButtonDown(0) && TowerSelect && hit.transform.tag != "Path")
        {
            Debug.Log("Tower has been placed");
            TowerSelect = false;
        }
    }

    private void ChangeObjectColor()
    {
        if (hit.transform == null) { return; }

        if (hit.transform.tag == "Path" && TowerSelect)
        {
            towerMat.color = Color.red;
        }
        else if (hit.transform.tag != "Path" && TowerSelect)
        {
            towerMat.color = Color.green;
        }
    }
}
