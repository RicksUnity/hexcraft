using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SelectedItem : MonoBehaviour 
{
	public int selectedItem = 0; 

	public List<Item> selected = new List<Item>(); 

	// Use this for initialization
	void Start () 
	{
		
		SelectItem(); 
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void SelectItem()
	{
		for(int i = 0; i<10; i++ )
		{
			print(i);
			print(Inventory.inventory[i].itemName); 

		}
		

		


	}
}