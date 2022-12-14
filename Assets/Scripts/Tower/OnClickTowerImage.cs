using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnClickTowerImage : MonoBehaviour
{
    [Header("BuyTower Settings")]
    [SerializeField] private int towerCost;
    [SerializeField][Range(0f, 1f)] private float imageApacityDisable;
    [Header("Tower Prefab")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private TMP_Text towerCostText;
    private GameObject towerParent;

    private float imageApacityEnable;
    private GameObject tower;
    private bool disable = false;
    private bool notEnoughMoney = false;
    private TowerShop towerShop;
    private Image towerImage;
    private Color towerImageColor;
    private Bank bank;

    private void Start()
    {
        towerParent = GameObject.FindGameObjectWithTag("towerParent");
        bank = FindObjectOfType<Bank>();
        towerImage = transform.GetComponent<Image>();
        imageApacityEnable = towerImage.color.a;
        towerImageColor = towerImage.color;
        towerShop = FindObjectOfType<TowerShop>();
        towerCostText.text = $"Cost : {towerCost}";
    }

    private void Update()
    {
        CheckBankBalance();
    }

    private void CheckBankBalance()
    {
        if (bank.bankBalance < towerCost && !notEnoughMoney)
        {
            notEnoughMoney = true;
            DisableTowerImage();
        }
        else if(bank.bankBalance >= towerCost && notEnoughMoney)
        {
            notEnoughMoney = false;
            EnableTowerImage();
        }
    }

    public void ClickedOnImage()
    {
        if (!disable)
        {
            tower = Instantiate(towerPrefab, towerParent.transform);
            tower.GetComponent<PlaceTower>().TowerSelected(true,towerCost);
            if (towerParent.transform.childCount > 0)
            {
                towerParent.BroadcastMessage("CantSelectTower");
            }
            towerShop.TowerHasBeenSelected();
        }
    }

    public void DisableTowerImage()
    {
        disable = true;
        towerImageColor.a = imageApacityDisable;
        towerImage.color = towerImageColor;    
    }

    public void EnableTowerImage()
    {
        notEnoughMoney = false;
        disable = false;
        towerImageColor.a = imageApacityEnable;
        towerImage.color = towerImageColor;
    }
}
