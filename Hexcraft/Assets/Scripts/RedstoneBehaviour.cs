using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedstoneBehaviour : MonoBehaviour {
    public int strength = 0;
    public bool torch = false;

	
	// Update is called once per frame
	void Update () {
        if (torch == false)
        {
            Collider[] nearby = Physics.OverlapSphere(transform.position, 2.1f);
            strength = 0;
            for (int i = 0; i < nearby.Length; i++)
            {
                if (nearby[i].name == "redstone(Clone)" && nearby[i].GetComponent<RedstoneBehaviour>().strength > strength + 1)
                {
                    strength = nearby[i].GetComponent<RedstoneBehaviour>().strength - 1;
                }

            }
            
        }
        else
        {
            if (GetComponent<DropMechanics>().isDropped != true)
            {
                if(GetComponent<DropMechanics>().attatchedTo.GetComponent<DropMechanics>().isPowered == false)
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
