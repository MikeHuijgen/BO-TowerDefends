using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour
{
    [Header("PlaceTower Settings")]
    [SerializeField] private float towerColliderX;
    [SerializeField] private float towerColliderZ;
    [Header("References")]
    [SerializeField] private BoxCollider towerCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Material rangeMaterial;

    public int towerCost;
    private float towerColliderY = 1f;
    private float towerRange;
    private float towerRangeColliderTrans;
    private float towerRangeOpacity;

    [System.NonSerialized] public bool isSelected = false;
    private bool isInTowerCollider = false;
    private bool mouseIsInUI = false;

    private List<Collider> towerCollides = new List<Collider>();
    private Bank bank;
    private TowerShop towerShop;
    private GameObject towerRangeTransform;
    private GameObject towerRangeCollider;
    private Vector3 mousePos;
    private Vector3 worldPos;
    private RaycastHit hit;
    private Color rangeColor;
    private ShowTowerInfo showTowerInfo;
    private Tower tower;
    private Transform towerParent;

    private void Awake()
    {
        // it takes some values from the tower script and after that disable the tower script
        towerParent = transform.parent;
        tower = GetComponent<Tower>();
        showTowerInfo = FindObjectOfType<ShowTowerInfo>();
        towerRangeTransform = tower.towerRangeTransform;
        towerRangeCollider = tower.towerRangeCollider;
        towerRange = tower.towerRangeSphere;
        towerRangeColliderTrans = tower.towerRangeColliderTrans;
        towerRangeOpacity = tower.rangeOpacity;
        foreach (Transform tower in towerParent)
        {
            // door deze lijn breekt mijn code moet nog aangepast worden 
            if (!tower.GetComponent<Tower>().playerCanSelect)
            {
                tower.GetComponent<Tower>().TowerGotDeselected();
            }
            showTowerInfo.TowerGotDeselected();
        }
        tower.enabled = false;
    }

    private void Start()
    {
        towerShop = FindObjectOfType<TowerShop>();
        bank = FindObjectOfType<Bank>();
        towerCollider.size = new Vector3(towerColliderX, towerColliderY, towerColliderZ);
        towerRangeTransform.transform.localScale = new Vector3(towerRange,0,towerRange);
        towerRangeCollider.transform.localScale = new Vector3(towerRangeColliderTrans, 0, towerRangeColliderTrans);
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
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                mouseIsInUI = true;
                return; 
            }

            //Gets the tower to the mouse position
            mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            towerRangeTransform.GetComponent<MeshRenderer>().enabled = true;

            if (Physics.Raycast(ray,out RaycastHit hitInfo, 100f, groundLayer))
            {
                mouseIsInUI = false;
                hit = hitInfo;
                worldPos = hitInfo.point;
            }
            // the y scale / 2 have the purpose to make sure that the tower always conects with the ground
            transform.position = new Vector3(worldPos.x,worldPos.y + (transform.localScale.y / 2),worldPos.z);
        }
    }

    private void PlaceTheTower()
    {
        //if you can place the tower you place the tower where your mouse is
        if (Input.GetMouseButtonDown(0) && isSelected && hit.transform.tag != "Path" && !isInTowerCollider && bank.bankBalance >= towerCost && !mouseIsInUI)
        {
            if (transform.parent.transform.childCount > 0)
            {
                transform.parent.BroadcastMessage("CanSelectTower");
            }
            GetComponentInChildren<SaveTowerPosition>().SaveCurrentPosition();
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
        // it checks if the tower is collided with a other tower
        if (other.gameObject.CompareTag("tower"))
        {
            towerCollides.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tower"))
        {
            towerCollides.Remove(other);
        }
    }

    private void CheckCollisonList()
    {
        // if there are 0 tower collides then you can place the tower otherwise you can't place the tower
        if (towerCollides.Count > 0)
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

        if (hit.transform.tag == "Path" || isInTowerCollider || mouseIsInUI)
        {
            rangeColor = Color.red;
            rangeColor.a = towerRangeOpacity;
            rangeMaterial.color = rangeColor;
        }
        else
        {
            rangeColor = Color.grey;
            rangeColor.a = towerRangeOpacity;
            rangeMaterial.color = rangeColor;
        }
    }

}
