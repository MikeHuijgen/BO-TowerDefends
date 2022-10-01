using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBewijzen : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(moveX, 0, 0));
        RayCast();
    }

    private void RayCast()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        if (Physics.Raycast(ray, out hit))
        {
            hit.transform.GetComponent<Renderer>().enabled = false;
            Debug.Log("Destory");
        }
    }
}
