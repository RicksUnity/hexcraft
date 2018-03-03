using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBlock : MonoBehaviour {
	
	void Update () {
        //Determines where the raycast is pointing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, hit.point);
        // If the raycast hits and object, and the left mouse button is down, destroy the gameObject
        if (Physics.Raycast(ray, out hit, 3f) && Input.GetMouseButton(0))
        {
            Destroy(hit.transform.gameObject);
        }
        if (Physics.Raycast(ray, out hit, 3f) && Input.GetMouseButton(1))
        {
            print(hit.normal);

        }
    }
}
