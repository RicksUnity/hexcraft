using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMechanics : MonoBehaviour {
    public GameObject player;
    public bool isDropped = false;
    public bool isPowered = false;
    public GameObject attatchedTo = null;
    public ItemDatabase ItemDatabase;
    public Inventory Inventory;

    void Update()
    {

        //If an item drop is close to the player make it attract towards them. Then delete when very close to player.
        if (isDropped == true && Vector3.Distance(player.transform.position, transform.position) <= 5f)
        {
            GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * 1000 * Time.smoothDeltaTime);
            //When drop is close to player add the item to players inventory and delete it from the world
            if (Vector3.Distance(player.transform.position,transform.position)<=2f)
            {
                for (int i = 0; i < ItemDatabase.items.Count; i++)
                {
                    if(gameObject.name == ItemDatabase.items[i].itemWorld)
                    {
                        Inventory.addItem(ItemDatabase.items[i].itemID);
                    }
                }
                    
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
        if (isPowered == true)
        {
            Collider[] nearby = Physics.OverlapSphere(transform.position, 2.1f);
            for(int i = 0; i < nearby.Length; i++)
            {
                if(nearby[i].name == "redstone(Clone)" && nearby[i].GetComponent<RedstoneBehaviour>().infront == gameObject && nearby[i].GetComponent<RedstoneBehaviour>().strength>0)
                {
                    return;
                }
            }
            isPowered = false;
        }
    }
}
