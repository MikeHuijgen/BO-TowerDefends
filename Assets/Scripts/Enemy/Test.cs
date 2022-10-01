using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int getID;
    public string myString;
    public int myInt;


    [System.Serializable]
    public struct balloon
    {
        public int id;
        public string balloonColor;
    }
    public balloon[] balloons;


    public Dictionary<int, string> testDic = new Dictionary<int, string>();

    private void Start()
    {
        foreach (var balloon in balloons)
        {
            testDic.Add(balloon.id, balloon.balloonColor);
        }
        myString = testDic[getID];

        Debug.Log(testDic[getID]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            getID--;
            if (!testDic.ContainsKey(getID)) { return; }
            myString = testDic[getID];
            Debug.Log(testDic[getID]);
        }
    }


}
