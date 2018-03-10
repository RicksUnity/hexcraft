using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public List<Item> inventory = new List<Item>();
	private ItemDatabase database; 
	void Start() {
		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>(); 
		
		print(database.items[0].itemName);
		print(database.items[1].itemName);
		print(inventory.Count);
		inventory.Add (database.items[0]);
		inventory.Add (database.items[1]);
		print(inventory[0].itemName);
		print(inventory[1].itemName);

	}
	void OnGUI(){
		for (int i = 0; i<inventory.Count; i++){
			GUI.Label(new Rect(10,i*20,200,50),inventory[i].itemName);
		}
	}


		
	}

