using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTowerPosition : MonoBehaviour
{
    public bool posIsLeft;
    public bool posIsRight;

    private Collider mapPosition;
    private bool savedPos = false;


    public void SaveCurrentPosition()
    {
        savedPos = true;
    }
    
    private void SavePosData()
    {
        if (mapPosition.GetComponent<Collider>().CompareTag("leftMapCollider"))
        {
            posIsLeft = true;
            mapPosition = null;
        }
        else if (mapPosition.GetComponent<Collider>().CompareTag("rightMapCollider"))
        {
            posIsRight = true;
            mapPosition = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (savedPos && (other.gameObject.CompareTag("leftMapCollider") || other.gameObject.CompareTag("rightMapCollider")))
        {
            savedPos = false;
            mapPosition = other.gameObject.GetComponent<Collider>();
            SavePosData();
        }
    }
}
