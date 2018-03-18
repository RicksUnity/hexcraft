using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedstoneBehaviour : MonoBehaviour {
    public int strength = 0;
    public bool torch = false;
    public Vector3 pointing = new Vector3(0,0,0);
    private Collider behind;
    public GameObject infront;
    public bool affected;	

    public void Orientation (bool passOn){
        pointing = new Vector3(0, 0, 0);
        Collider[] nearby = Physics.OverlapSphere(transform.position, 2.1f);
        for (int j = 0; j < nearby.Length; j++)
        {
            if ((nearby[j].name == "redstone(Clone)" || nearby[j].name == "redstoneTorch(Clone)") && nearby[j].transform.position != transform.position && nearby[j].GetComponent<DropMechanics>().isDropped == false)
            {
                if (pointing != new Vector3(0, 0, 0))
                {

                    pointing = new Vector3(0, 0, 0);
                    print("break"+transform.position);
                    if (infront != null)
                    {
                        print("destroy infront" + infront.transform.position);
                        //infront.GetComponent<DropMechanics>().isPowered -= 1;
                        infront = null;
                        affected = false;
                    }
                    break;
                    
                }
                else
                {
                    pointing = transform.position - nearby[j].transform.position;
                    print("once"+transform.position);
                    for (int k = 0; k<nearby.Length; k++)
                    {
                        if(nearby[k].transform.position == (transform.position + pointing) && nearby[k].name != "redstone(Clone)" && nearby[k].name != "redstoneTorch(Clone)")
                        {
                            infront = nearby[k].transform.gameObject;
                            if (strength >0 && affected == false)
                            {
                                infront.GetComponent<DropMechanics>().isPowered = true;
                                affected = true;
                                print(infront.transform.position + "bub" + infront.GetComponent<DropMechanics>().isPowered);
                            }
                            
                        }
                    }

                }
            }
        }
        if (passOn)
        {
            for (int l = 0; l < nearby.Length; l++)
            {
                if (nearby[l].name == "redstone(Clone)" && nearby[l].transform.position != transform.position)
                {
                    nearby[l].GetComponent<RedstoneBehaviour>().Orientation(false);
                }
            }
        }
    }
    // Update is called once per frame
    void Update () {
        Collider[] nearby = Physics.OverlapSphere(transform.position, 2.1f);
        if (torch == false)
        {
            strength = 0;

            for (int i = 0; i < nearby.Length; i++)
            {
                if ((nearby[i].name == "redstone(Clone)" || nearby[i].name == "redstoneTorch(Clone)") && nearby[i].GetComponent<RedstoneBehaviour>().strength > strength + 1)
                {
                    strength = nearby[i].GetComponent<RedstoneBehaviour>().strength - 1;
                }
            }
            if (strength > 0 && affected == false && infront != null)
            {
                infront.GetComponent<DropMechanics>().isPowered = true;
                affected = true;
                print(infront.transform.position + "bub" + infront.GetComponent<DropMechanics>().isPowered);
            }
            if (strength == 0 && affected == true && infront != null)
            {
                //infront.GetComponent<DropMechanics>().isPowered -= 1;
                affected = false;
                print("off");
            }
        }
        else
        {
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
