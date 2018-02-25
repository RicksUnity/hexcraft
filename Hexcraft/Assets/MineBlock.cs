using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBlock : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, hit.point);
        if (Physics.Raycast(ray, out hit, 1.1f) & hit.transform.gameObject.tag == "Finish" & Input.GetMouseButton(0))
        {
            Destroy(hit.transform.gameObject);
        }

    }
}
