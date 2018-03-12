using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMechanics : MonoBehaviour {
    public GameObject player;
    public bool isDropped = false;

	void Update () {
        //If an item drop is close to the player make it attract towards them. Then delete when very close to player.
		if(isDropped == true && Vector3.Distance(player.transform.position, transform.position) <= 5f)
        {
            GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * 1000 * Time.smoothDeltaTime);
            if (Vector3.Distance(player.transform.position,transform.position)<=1.5f)
            {
                Destroy(gameObject);
            }
        }
	}
}
