using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTargetStyleButton : MonoBehaviour
{
    private Tower tower;
    private TMP_Text buttonText;
    public bool isOnFirst;
    public bool isOnLast;

    private bool targetStyleChecked = false;

    private void Start()
    {
        buttonText = GetComponentInChildren<TMP_Text>();    
    }

    private void Update()
    {
        ChangeUI();
    }

    private void ChangeUI()
    {
        if (isOnFirst)
        {
            tower.ChangeTargetStyle(Tower.TargetStyle.first);
            buttonText.text = "First";
        }
        else if (isOnLast)
        {
            tower.ChangeTargetStyle(Tower.TargetStyle.last);
            buttonText.text = "Last";
        }
    }

    public void GetTowerGameobject(Tower _tower)
    {
        if (_tower != tower)
        {
            targetStyleChecked = false;
        }
        tower = _tower;
        CheckTargetStyle();
    }

    private void CheckTargetStyle()
    {
        if (!targetStyleChecked)
        {
            if (tower.targetStyle == Tower.TargetStyle.first)
            {
                isOnFirst = true;
            }
            else
            {
                isOnLast = true;
            }

            targetStyleChecked = true;
        }
    }


    public void OnClickTargetChange()
    {
        if (isOnFirst)
        {
            isOnFirst = false;
            isOnLast = true;
        }
        else if (isOnLast)
        {
            isOnLast = false;
            isOnFirst = true;
        }
    }
}
