using System.Collections;
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
			//When mined, block becomes smaller and rotates, then a rigidbody is added
			hit.transform.localScale = hit.transform.localScale/5;
			hit.transform.Rotate (0, 90, 45);
			if (hit.transform.gameObject.GetComponent<Rigidbody> () == null) 
			{
				hit.transform.gameObject.GetComponent<MeshCollider> ().convex = true;
				hit.transform.gameObject.AddComponent<Rigidbody> ().useGravity = true;
			}
			//sets up properties of the block drop
			hit.transform.gameObject.GetComponent<DropMechanics>().isDropped = true;
			hit.transform.gameObject.GetComponent<DropMechanics> ().player = gameObject;
			hit.transform.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			hit.transform.gameObject.GetComponent<Rigidbody> ().AddRelativeTorque (new Vector3 (3, 0, 0));

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
