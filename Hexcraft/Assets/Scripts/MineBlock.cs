﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBlock : MonoBehaviour {

    public GameObject placeBlock;

	void Update () {
        //Determines where the raycast is pointing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        // If the raycast hits and object, and the left mouse button is down, destroy the gameObject
        if (Physics.Raycast(ray, out hit, 8f) && Input.GetMouseButtonDown(0))
        {
            Destroy(hit.transform.gameObject);
        }
        if (Physics.Raycast(ray, out hit, 8f) && Input.GetMouseButtonDown(1))
        {
            //Places a new Hexagon of choice
            GameObject newHex = Instantiate(placeBlock);
            //The following ifs transform hex into position in direction of the normal of where the Raycast hits the original hex
            if(hit.normal == new Vector3(1f,0f,0f))
            {
                newHex.transform.position = hit.transform.position + new Vector3(2, 0, 0);
            }
            if (hit.normal == new Vector3(0.5000005f, 0, -0.86602515f))
            { 
                newHex.transform.position = hit.transform.position + new Vector3(1f, 0, -1.732051f);
            }
            if(hit.normal == new Vector3(-0.5000005f, 0, -0.86602515f))
            {
                newHex.transform.position = hit.transform.position + new Vector3(-1f, 0, -1.732051f);
            }
            if (hit.normal == new Vector3(-1.0f, 0.0f, 0.0f))
            {
                newHex.transform.position = hit.transform.position + new Vector3(-2f, 0, 0);

            }
            if (hit.normal == new Vector3(-0.5000005f, 0, 0.86602515f))
            {
                newHex.transform.position = hit.transform.position + new Vector3(-1f, 0, 1.732051f);
            }
            if (hit.normal == new Vector3(0.5000005f, 0, 0.86602515f))
            {
                newHex.transform.position = hit.transform.position + new Vector3(1f, 0, 1.732051f);
            }
            if (hit.normal == new Vector3(0f, 1f, 0f))
            {
                newHex.transform.position = hit.transform.position + new Vector3(0, 2, 0);
            }
            if (hit.normal == new Vector3(0f, -1f, 0f))
            {
                newHex.transform.position = hit.transform.position + new Vector3(2, -2, 0);
            }
        }
    }
}