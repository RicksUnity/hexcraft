using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMechanics : MonoBehaviour {
    public GameObject player;
    public bool isDropped = false;
    public ItemDatabase database;
    public Inventory invent;


    void Update () {
        //If an item drop is close to the player make it attract towards them.
		if(isDropped == true && Vector3.Distance(player.transform.position, transform.position) <= 5f)
        {
            GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * 1000 * Time.smoothDeltaTime);
            //When drop is close to player add the item to players inventory and delete it from the world
            if (Vector3.Distance(player.transform.position,transform.position)<=2f)
            {
                for (int i = 0; i < database.items.Count; i++)
                {
                    if(gameObject.name == database.items[i].itemWorld)
                    {
                        invent.addItem(database.items[i].itemID);
                    }
                }
                    
                Destroy(gameObject);
            }
        }
	}
}
