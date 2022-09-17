using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    [Header("PlaceTower Settings")]
    [SerializeField] private float towerGroundHight;
    [SerializeField] private float towerColliderX;
    [SerializeField] private float towerColliderZ;
    [Header("References")]
    [SerializeField] private BoxCollider towerCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Material rangeMat;

    private int towerCost;
    private float towerColliderY = 1f;
    private float towerRange;
    private float towerRangeOpacity;

    private bool isSelected = false;
    private bool isInTowerCollider = false;

    private List<Collider> colliderChecker = new List<Collider>();
    private Bank bank;
    private TowerShop towerShop;
    private GameObject towerRangeTransform;
    private Vector3 mousePos;
    private Vector3 worldPos;
    private RaycastHit hit;
    private Color rangeColor;
    private Tower tower;

    private void Awake()
    {
        // it takes some values from the tower script and after that disable the tower script
        tower = GetComponent<Tower>();
        towerRangeTransform = tower.towerRangeTransform;
        towerRange = tower.towerRange;
        towerRangeOpacity = tower.rangeOpacity;
        tower.enabled = false;
    }

    private void Start()
    {
        towerShop = FindObjectOfType<TowerShop>();
        bank = FindObjectOfType<Bank>();
        towerCollider.size = new Vector3(towerColliderX, towerColliderY, towerColliderZ);
        towerRangeTransform.transform.localScale = new Vector3(towerRange,0,towerRange);
    }

    private void Update()
    {
        PlaceTheTower();
        TowerToMousePos();
        ChangeObjectColor();
        CheckCollisonList();
        CancelTowerPlacement();
    }
    public void TowerSelected(bool selected, int costTower)
    {
        towerCost = costTower;
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
        if (Input.GetMouseButtonDown(0) && isSelected && hit.transform.tag != "Path" && !isInTowerCollider && bank.bankBalance > towerCost)
        {
            bank.DecreaseBankAmount(towerCost);
            towerShop.TowerHasBeenPlaced();
            towerRangeTransform.GetComponent<MeshRenderer>().enabled = false;
            isSelected = false;

            tower.enabled = true;
            PlaceTower placeTower = this;
            placeTower.enabled = false;
        }
    }

    private void CancelTowerPlacement()
    {
        if (Input.GetMouseButtonDown (1))
        {
            towerShop.TowerHasBeenPlaced();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tower"))
        {
            colliderChecker.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tower"))
        {
            colliderChecker.Remove(other);
        }
    }

    private void CheckCollisonList()
    {
        if (colliderChecker.Count > 0)
        {
            isInTowerCollider = true;
        }
        else
        {
            isInTowerCollider = false;
        }
    }

    private void ChangeObjectColor()
    {
        //it checks if you can place the tower and change the color
        if (hit.transform == null) { return; }

        if (hit.transform.tag == "Path" || isInTowerCollider)
        {
            rangeColor = Color.red;
            rangeColor.a = towerRangeOpacity;
            rangeMat.color = rangeColor;
        }
        else
        {
            rangeColor = Color.grey;
            rangeColor.a = towerRangeOpacity;
            rangeMat.color = rangeColor;
        }
    }

}
