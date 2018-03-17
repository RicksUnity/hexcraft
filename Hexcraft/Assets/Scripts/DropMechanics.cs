using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMechanics : MonoBehaviour {
    public GameObject player;
    public bool isDropped = false;
    public bool isPowered = false;
    public GameObject attatchedTo = null;

    void Update()
    {

        //If an item drop is close to the player make it attract towards them. Then delete when very close to player.
        if (isDropped == true && Vector3.Distance(player.transform.position, transform.position) <= 5f)
        {
            GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * 1000 * Time.smoothDeltaTime);
            if (Vector3.Distance(player.transform.position, transform.position) <= 2f)
            {
                Destroy(gameObject);
            }
        }
        if (attatchedTo != null)
        {
            if (attatchedTo.GetComponent<DropMechanics>().isDropped == true)
            {
                transform.localScale = transform.localScale / 5;
                transform.Rotate(0, 90, 45);
                if (transform.gameObject.GetComponent<Rigidbody>() == null)
                {
                    transform.gameObject.GetComponent<MeshCollider>().convex = true;
                    transform.gameObject.AddComponent<Rigidbody>().useGravity = true;
                }
                //sets up properties of the block drop
                transform.gameObject.GetComponent<DropMechanics>().isDropped = true;
                transform.gameObject.GetComponent<DropMechanics>().player = gameObject;
                transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                transform.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(3, 0, 0));
                attatchedTo = null;
            }

        }
    }
}
