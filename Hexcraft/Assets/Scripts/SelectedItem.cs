using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SelectedItem : MonoBehaviour 
{
	public float damage; 
	public float speed; 
	public int selectedItem = 1; 

	public List<Item> selected = new List<Item>(); 

	// Use this for initialization
	void Start () 
	{
		SelectItem(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxis("Mouse ScrollWheel") > 0f )
		{
			//print("Up");
			if(selectedItem >= 10)
			{
				selectedItem = 0;
				SelectItem();  
			}
			else
			{
			selectedItem++; 
			SelectItem(); 
		}

		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0f )
		{
			//print("Down");
			if(selectedItem <= 0)
			{
				selectedItem = 9;
				SelectItem(); 
			}
			else
			{
			selectedItem--; 
			SelectItem(); 
		}

		}
		//if(Input.GetKeyDown)
	}

	void SelectItem()
	{
		//print("Hey Dude");

	
		for(int i = 0; i<10; i++ )
		{
			if(i == selectedItem)
			{
				print(Inventory.inventory[i].itemName); 
			}	

		}
	}
	void UseItem()
	{

	}

}
