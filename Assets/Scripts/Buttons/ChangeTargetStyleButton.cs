using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTargetStyleButton : MonoBehaviour
{
    private Tower tower;
    private TMP_Text buttonText;
    private bool isOnFirst;
    private bool isOnLast;

    private bool targetStyleChecked = false;

    private void Start()
    {
        buttonText = GetComponentInChildren<TMP_Text>();    
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
                OnClickTargetChange();
            }
            else
            {
                isOnLast = true;
                OnClickTargetChange();
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
            tower.ChangeTargetStyle(Tower.TargetStyle.first);
            buttonText.text = "First";
        }
        else if (isOnLast)
        {
            isOnLast = false;
            isOnFirst = true;
            tower.ChangeTargetStyle(Tower.TargetStyle.last);
            buttonText.text = "Last";
        }
    }
}
