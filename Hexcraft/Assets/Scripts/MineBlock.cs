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
                hit.transform.GetComponent<MOBcontroller>().health -= (12 + SelectedItem.GetComponent<SelectedItem>().itemDamage);
                hit.transform.GetComponent<Rigidbody>().AddForce(new Vector3(hit.transform.position.x - transform.position.x, 1, hit.transform.position.z - hit.transform.position.z));
            }
            // If the raycast hits and object, and the left mouse button is down, start mining the block
            else
            {
                mineCounter += 1;
                //If the mine counter gets ocver a certain value then mine the block
                if (mineCounter >= mineSpeed)
                {
                    //When mined, block becomes smaller and rotates, then a rigidbody is added
                    hit.transform.gameObject.GetComponent<DropMechanics>().isDropped = true;
                    //If the block being mined is redstone then make all nearby redstone run their orientation check to see if a line has been broken.
                    if (hit.transform.name == "redstone(Clone)" || hit.transform.name == "redstoneTorch(Clone)")
                    {
                        hit.transform.GetComponent<RedstoneBehaviour>().Orientation(true);
                    }
                    hit.transform.localScale = hit.transform.localScale / 5;
                    hit.transform.Rotate(0, 90, 45);
                    hit.transform.position += new Vector3(0f, 0.5f, 0);
                    if (hit.transform.gameObject.GetComponent<Rigidbody>() == null)
                    {
                        hit.transform.gameObject.GetComponent<MeshCollider>().convex = true;
                        hit.transform.gameObject.AddComponent<Rigidbody>().useGravity = true;
                    }
                    hit.transform.gameObject.GetComponent<DropMechanics>().isDropped = true;
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

                //The following ifs transform hex into position in direction of the normal of where the Raycast hits the original hex
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
                    newHex.transform.position += new Vector3(0, -1, 0);
                    Collider[] nearbyRed = Physics.OverlapSphere(newHex.transform.position, 1.1f);
                    for (int i = 0; i < nearbyRed.Length; i++)
                    {
                        if(nearbyRed[i].transform.position == newHex.transform.position + new Vector3(0,-1,0))
                        {
                            newHex.GetComponent<DropMechanics>().attatchedTo = hit.transform.gameObject;
                        }
                    }
                    if (newHex.GetComponent<DropMechanics>().attatchedTo == null)
                    {
                        Destroy(newHex);
                    }
                }
                if (newHex.name == "RedstoneTorch(Clone)")
                {
                    newHex.transform.localScale = newHex.transform.localScale / 3;
                    newHex.transform.position = newHex.transform.position + (hit.transform.position - newHex.transform.position) / 2;
                    newHex.GetComponent<DropMechanics>().attatchedTo = hit.transform.gameObject;
                }
                if (hit.transform.gameObject.name == "redstone(Clone)")
                {
                    newHex.transform.position += new Vector3(0, 1, 0);
                }
                if ((newHex.GetComponent<Collider>().bounds.Intersects(playerCollider.GetComponent<Collider>().bounds)) || ((newHex.name == "redstone(Clone)" || newHex.name == "redstoneTorch(Clone)") && hit.transform.gameObject.name == "redstone(Clone)")||(hit.transform.gameObject.name == "redstoneTorch(Clone)"))
                {
                    Destroy(newHex);
                }
                else
                { 
                    Collider[] nearby = Physics.OverlapSphere(newHex.transform.position, 0.05f);
                    for (int j = 0; j < nearby.Length; j++)
                    {
                        if ((nearby[j].transform != newHex.transform && nearby[j].transform.position == newHex.transform.position) )
                        {
                            Destroy(newHex);
                        }
                        else
                        {
                            Inventory.RemoveItem(placeBlockID);
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

    void MineBar()
    {
        mineBar.sizeDelta = new Vector2(mineCounter, mineBar.sizeDelta.y);
        mineBar.anchoredPosition = new Vector2(-(mineSpeed - mineCounter) / 2, 0);
    }
}
