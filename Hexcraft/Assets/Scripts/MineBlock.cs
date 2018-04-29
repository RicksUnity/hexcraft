using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBlock : MonoBehaviour {

    public GameObject placeBlock;
    public ItemDatabase ItemDatabase;
    public Inventory Inventory;
    public GameObject playerCollider;
    public SelectedItem SelectedItem;
    private int placeBlockID = -1;
    public float mineCounter = 0;
    public float mineSpeed = 10; //Lower number means a higher speed.
    public RectTransform mineBar;

	void Update () {
        MineBar();
        //Determines where the raycast is pointing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;

        //Reset the mining counter if the mouse button is ever released
        if(Input.GetMouseButtonUp(0))
        {
            mineCounter = 0;
        }
        
        if (Physics.Raycast(ray, out hit, 8f) && Input.GetMouseButton(0))
        {
            //If the raycast is hitting an enemy, then attack the enemy and knock it back
            if (hit.transform.gameObject.tag == "Enemy" && Input.GetMouseButtonDown(0))
            {
                print("bingo");
                hit.transform.GetComponent<MOBcontroller>().health -= (12  );// + SelectedItem.GetComponent<SelectedItem>().itemDamage);
                hit.transform.GetComponent<Rigidbody>().AddForce(new Vector3(hit.transform.position.x - transform.position.x, 1, hit.transform.position.z - hit.transform.position.z));
            }
            // If the raycast hits and object, and the left mouse button is down, start mining the block
            else
            {
                mineCounter += 1;
                //If the mine counter gets ocver a certain value then mine the block
                if (mineCounter >= mineSpeed)
                {
                    
                    hit.transform.gameObject.GetComponent<DropMechanics>().isDropped = true;
                    //If the block being mined is redstone then make all nearby redstone run their orientation check to see if a line has been broken.
                    if (hit.transform.name == "redstone(Clone)" || hit.transform.name == "redstoneTorch(Clone)")
                    {
                        hit.transform.GetComponent<RedstoneBehaviour>().Orientation(true);
                    }
                    //When mined, block becomes smaller and rotates, then a rigidbody is added
                    hit.transform.localScale = hit.transform.localScale / 5;
                    hit.transform.Rotate(0, 90, 45);
                    hit.transform.position += new Vector3(0f, 0.5f, 0);
                    if (hit.transform.gameObject.GetComponent<Rigidbody>() == null)
                    {
                        if (hit.transform.gameObject.GetComponent<MeshCollider>() != null)
                        {
                            hit.transform.gameObject.GetComponent<MeshCollider>().convex = true;
                        }
                        hit.transform.gameObject.AddComponent<Rigidbody>().useGravity = true;
                    }
                    hit.transform.gameObject.GetComponent<DropMechanics>().isDropped = true;
                    //sets the player reference to be the player capsule so that it can be picked up
                    hit.transform.gameObject.GetComponent<DropMechanics>().player = gameObject;
                    hit.transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(3, 0, 0));
                    hit.transform.gameObject.GetComponent<DropMechanics>().ItemDatabase = ItemDatabase;
                    hit.transform.gameObject.GetComponent<DropMechanics>().Inventory = Inventory;
                    mineCounter = 0;
                }
            }
        }

        if (Physics.Raycast(ray, out hit, 8f) && Input.GetMouseButtonDown(1))
        {
            //determines the id of the block that is being placed
            for (int i = 0; i < ItemDatabase.items.Count; i++)
            {
                //if (placeBlock.name + "(Clone)" == ItemDatabase.items[i].itemWorld)
                if (placeBlock == ItemDatabase.items[i].itemWorld)
                {
                    placeBlockID = ItemDatabase.items[i].itemID;
                }
            }
            //if (Inventory.InventoryContains(placeBlockID) && placeBlockID != -1)
            if(true)
            {
                //Places a new Hexagon of choice
                GameObject newHex = Instantiate(placeBlock);

                //The following ifs transform hex into position in direction of the normal of the surface of where the Raycast hits the original hex
                if (hit.normal == new Vector3(1f, 0f, 0f))
                {
                    newHex.transform.position = hit.transform.position + new Vector3(2, 0, 0);
                }
                if (hit.normal == new Vector3(0.5000005f, 0, -0.86602515f))
                {
                    newHex.transform.position = hit.transform.position + new Vector3(1f, 0, -1.732051f);
                }
                if (hit.normal == new Vector3(-0.5000005f, 0, -0.86602515f))
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

                
                if (newHex.name == "redstone(Clone)")
                {
                    //Make redstone go onto the ground, not hover.
                    newHex.transform.position += new Vector3(0, -1, 0);
                    //makes the redstone attatched to the block underneath it so that when it is mined, the redstone is as well.
                    Collider[] nearbyRed = Physics.OverlapSphere(newHex.transform.position, 1.1f);
                    for (int i = 0; i < nearbyRed.Length; i++)
                    {
                        if(nearbyRed[i].transform.position == newHex.transform.position + new Vector3(0,-1,0))
                        {
                            newHex.GetComponent<DropMechanics>().attatchedTo = hit.transform.gameObject;
                            newHex.GetComponent<DropMechanics>().player = gameObject;
                        }
                    }
                    //If the redstone is not resting on anything then delete it
                    if (newHex.GetComponent<DropMechanics>().attatchedTo == null)
                    {
                        Destroy(newHex);
                    }
                }

                //If it is a redstone torch that is being placed then attatch it to the object being placed and make it smaller
                if (newHex.name == "redstoneTorch(Clone)" || newHex.name == "Torch(Clone)")
                {
                    newHex.transform.localScale = newHex.transform.localScale / 3;
                    newHex.transform.position = newHex.transform.position + (hit.transform.position - newHex.transform.position) / 2;
                    newHex.GetComponent<DropMechanics>().attatchedTo = hit.transform.gameObject;
                }

                //If you place a block onto redstone then fix the positioning of the block so it is not at half height
                if (hit.transform.gameObject.name == "redstone(Clone)")
                {
                    newHex.transform.position += new Vector3(0, 1, 0);
                }

                //the collider of the palced block is ontop of the player then delete the hex
                if ((newHex.GetComponent<Collider>().bounds.Intersects(playerCollider.GetComponent<Collider>().bounds)) || ((newHex.name == "redstone(Clone)" || newHex.name == "redstoneTorch(Clone)") && hit.transform.gameObject.name == "redstone(Clone)")||(hit.transform.gameObject.name == "redstoneTorch(Clone)"))
                {
                    Destroy(newHex);
                }
                else
                { 
                    //Don't allow a block to be placed in the same place as another. This is especcially necessary for placing blocks ontop of redstone
                    Collider[] nearby = Physics.OverlapSphere(newHex.transform.position, 0.05f);
                    for (int j = 0; j < nearby.Length; j++)
                    {
                        if ((nearby[j].transform != newHex.transform && nearby[j].transform.position == newHex.transform.position) )
                        {
                            Destroy(newHex);
                        }
                        //If a block has been placed, remove it from the players inventory
                        else
                        {
                            //Inventory.RemoveItem(placeBlockID);
                            //If the block paced is redstone then run the orientation check on thi and all nearby redstone.
                            if (newHex.name == "redstone(Clone)" || newHex.name == "redstoneTorch(Clone)")
                            {
                                newHex.GetComponent<RedstoneBehaviour>().Orientation(true);
                            }
                        }
                    }
                }
            }
        }
    }
    //change the shape of the mineBar to reflect the mineCounter
    void MineBar()
    {
        mineBar.sizeDelta = new Vector2(mineCounter, mineBar.sizeDelta.y);
        mineBar.anchoredPosition = new Vector2(-(mineSpeed - mineCounter) / 2, 0);
    }
}
