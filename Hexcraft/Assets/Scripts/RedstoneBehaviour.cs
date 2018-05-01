using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedstoneBehaviour : MonoBehaviour {
    public int strength = 0;
    public bool torch = false;
    public Vector3 pointing = new Vector3(0,0,0);
    private Collider behind;
    public GameObject infront;
    public Light lightSource;

    // --- Determines which way a line of redstone is facing --- 
    public void Orientation (bool passOn){
        pointing = new Vector3(0, 0, 0);
        Collider[] nearby = Physics.OverlapSphere(transform.position, 2.9f);
        //For every block close to the redstone
        for (int j = 0; j < nearby.Length; j++)
        {
            if (nearby[j].transform.position.y - transform.position.y > 1 || nearby[j].transform.position.y - transform.position.y < -1 || Mathf.Sqrt(Mathf.Pow(nearby[j].transform.position.x - transform.position.x, 2) + Mathf.Pow(nearby[j].transform.position.z - transform.position.z, 2)) < 2.1)
            {
                //If the nearby block is redstone and has not be mined
                if ((nearby[j].name == "redstone(Clone)" || nearby[j].name == "redstoneTorch(Clone)") && nearby[j].transform.position != transform.position && nearby[j].GetComponent<DropMechanics>().isDropped == false)
                {
                    //If pointing is not empty and it is another redstone detected, it means that there is at least two redstone surround the block so set pointng to empty
                    if (pointing != new Vector3(0, 0, 0))
                    {

                        pointing = new Vector3(0, 0, 0);
                        if (infront != null)
                        {
                            infront = null;
                        }
                        break;

                    }
                    //If this is the first redstone detected nearby, set pointing to the direction the redotn is pointing and set infront to the block infront
                    else
                    {
                        pointing = transform.position - nearby[j].transform.position;


                    }
                }
            }
        }
        // --- This causes all nearby redstone blocks to run the Orientation function ---
        if (passOn)
        {
            for (int l = 0; l < nearby.Length; l++)
            {
                if (nearby[l].transform.position.y - transform.position.y > 1 || nearby[l].transform.position.y - transform.position.y < -1 || Mathf.Sqrt(Mathf.Pow(nearby[l].transform.position.x - transform.position.x, 2) + Mathf.Pow(nearby[l].transform.position.z - transform.position.z, 2)) < 2.1)
                {
                    if (nearby[l].name == "redstone(Clone)" && nearby[l].transform.position != transform.position)
                    {
                        nearby[l].GetComponent<RedstoneBehaviour>().Orientation(false);
                    }
                }
            }
        }
    }

    void Update () {
        if (torch)
        {
            lightSource.intensity = strength;
        }
        // --- If a line of redsotne is pointing at a non redstone block, then the infront tag of the redstone becomes this block and the block becomes powered ---
        Collider[] nearby = Physics.OverlapSphere(transform.position, 2.9f);
        if (pointing != new Vector3(0,0,0))
        {
            for (int k = 0; k < nearby.Length; k++)
            {
                if (nearby[k].transform.position.y - transform.position.y > 1 || nearby[k].transform.position.y - transform.position.y < -1 || Mathf.Sqrt(Mathf.Pow(nearby[k].transform.position.x - transform.position.x, 2) + Mathf.Pow(nearby[k].transform.position.z - transform.position.z, 2)) < 2.1)
                {
                    if (nearby[k].transform.position == (transform.position + pointing + new Vector3(0, 1, 0)) && nearby[k].name != "redstone(Clone)" && nearby[k].name != "redstoneTorch(Clone)")
                    {
                        infront = nearby[k].transform.gameObject;
                        if (strength > 0)
                        {
                            infront.GetComponent<DropMechanics>().isPowered = true;
                            //infront.GetComponent<DropMechanics>().poweredStrength = strength - 1;
                        }

                    }
                }
            }
        }
     
        if (torch == false)
        {
            // --- Causes powered redstone to power touching redstone wth decreasig strength ---
            strength = 0;
            Vector3 pos = transform.position;
            for (int i = 0; i < nearby.Length; i++)
            {
                Vector3 near = nearby[i].transform.position;
                //Last statement is so that redstone also travels up and down blocks
                if ((nearby[i].name == "redstone(Clone)" || nearby[i].name == "redstoneTorch(Clone)") && (nearby[i].transform.position.y - transform.position.y > 1 || nearby[i].transform.position.y - transform.position.y < -1.1 || (Mathf.Sqrt(Mathf.Pow(nearby[i].transform.position.x - transform.position.x, 2) + Mathf.Pow(nearby[i].transform.position.z - transform.position.z, 2)) < 2.1 && transform.position.y - nearby[i].transform.position.y < 0.1)))
                {
                    if ( nearby[i].GetComponent<RedstoneBehaviour>().strength > strength + 1)
                    {
                        if (nearby[i].transform.position.y - transform.position.y < -1.1 && Physics.CheckSphere(pos - (pos - near)/2 - (pos - near)/5 , 0.1f))
                        {
                            continue;
                        }
                        else
                        {

                            strength = nearby[i].GetComponent<RedstoneBehaviour>().strength - 1;
                        }
                    }
                }
            }
            //--- If the strength goes above zero then the infront block is powered---
            if (strength > 0 && infront != null)
            {
                infront.GetComponent<DropMechanics>().isPowered = true;
            }
            //--- If the strength is above zero, then the block that the redstone is ontop of (and attatched to) becomes powered---
            if(strength >0 )
            {
                GetComponent<DropMechanics>().attatchedTo.GetComponent<DropMechanics>().isPowered = true;
            }
        }
        else
        {
            // --- If the block the the torch is on is powered then it tunrs off, otherwise its strength is 12
            if (GetComponent<DropMechanics>().isDropped != true)
            {
                if (GetComponent<DropMechanics>().attatchedTo.GetComponent<DropMechanics>().isPowered == false)
                {
                    strength = 12;
                }
                else
                {
                    strength = 0;
                }
            }
        }
    }
}
