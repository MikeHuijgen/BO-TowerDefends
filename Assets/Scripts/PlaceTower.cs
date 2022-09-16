using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField][Range(5,30)] private float towerRange;
    [SerializeField][Range(0,1)] private float rangeOpacity;
    [SerializeField] private float towerGroundHight;
    [Header("References")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Material rangeMat;
    [SerializeField] GameObject towerRangeTransform;

    private bool isSelected = false;
    private Vector3 mousePos;
    private Vector3 worldPos;
    private RaycastHit hit;
    private Color rangeColor;

    private void Start()
    {
        towerRangeTransform.transform.localScale = new Vector3(towerRange,0,towerRange);
    }

    private void Update()
    {
        PlaceTheTower();
        TowerToMousePos();
        ChangeObjectColor();
    }
    public void TowerSelected(bool selected)
    {
        isSelected = selected;
    }

    private void TowerToMousePos()
    {
        if (isSelected)
        {
            //Gets the tower to the mouse position
            mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            towerRangeTransform.GetComponent<MeshRenderer>().enabled = true;

            if (Physics.Raycast(ray,out RaycastHit hitInfo, 100f, groundLayer))
            {
                hit = hitInfo;
                worldPos = hitInfo.point;
            }

            transform.position = new Vector3(worldPos.x,towerGroundHight,worldPos.z);
        }
    }

    private void PlaceTheTower()
    {
        //if you can place the tower you place the tower where your mouse is
        if (Input.GetMouseButtonDown(0) && isSelected && hit.transform.tag != "Path")
        {
            towerRangeTransform.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("Tower has been placed");
            isSelected = false;
        }
    }


    private void ChangeObjectColor()
    {
        //it checks if you can place the tower and change the color
        if (hit.transform == null) { return; }

        if (hit.transform.tag == "Path" && isSelected)
        {
            rangeColor = Color.red;
            rangeColor.a = rangeOpacity;
            rangeMat.color = rangeColor;
        }
        else if (hit.transform.tag != "Path" && isSelected)
        {
            rangeColor = Color.grey;
            rangeColor.a = rangeOpacity;
            rangeMat.color = rangeColor;
        }
    }
}
