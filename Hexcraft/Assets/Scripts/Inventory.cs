using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>(); 
	private bool showInventory;   
	private ItemDatabase database; 
	private bool showToolTip; 
	private string tooltip; 
	private bool draggingItem; 
	private Item draggedItem; 
	private int prevIndex; 
	public static Inventory inventory2;
	void Start() {
		for (int i = 0; i < (slotsX*slotsY); i++){
			slots.Add(new Item());
			inventory.Add (new Item());
		}
		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>(); 
		//addItem(1);
		//addItem(0);
	}
	void Update(){
		if(Input.GetButtonDown("Inventory")){
			showInventory = !showInventory;
		}
	}

	void OnGUI(){
		//Debug.Log (inventory[0].itemID);
		tooltip = "";
		GUI.skin = skin;  
		if(showInventory){
			DrawInventory();
			if (showToolTip){
			GUI.Box (new Rect(Event.current.mousePosition.x +20f, Event.current.mousePosition.y + 20f, 200, 100), tooltip, skin.GetStyle("ToolTip"));
		}
		if (draggingItem){
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x +5f, Event.current.mousePosition.y + 5f, 50, 50), draggedItem.itemIcon);

		}
		
		}
		
		}
	
	void DrawInventory(){
		Event e = Event.current; 
		int i = 0; 
		GUI.BeginGroup (new Rect (Screen.width / 2 - 250, Screen.height / 3 - 50, 600, 250));
		GUI.Box(new Rect(0,0,600,250), "\n<color=#0>Glorious Inventory!</color>", skin.GetStyle("Background"));
		for (int y = 1; y < slotsY+1; y ++){
			for (int x = 1; x < slotsX+1; x++){
				Rect slotRect = new Rect(x * 50, y * 50, 50, 50); 
				GUI.Box(new Rect(slotRect), "", skin.GetStyle("Slot"));
				slots[i] = inventory[i];
				if (slots[i].itemName != null){
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					if (slotRect.Contains(e.mousePosition)){
						tooltip = CreateToolTip(slots[i]);
						showToolTip = true; 
						if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem){
							draggingItem = true;
							prevIndex = i; 
							draggedItem  = slots[i]; 
							inventory[i] = new Item();

						}
						
						if (e.type == EventType.MouseUp && draggingItem){
							inventory[prevIndex] = inventory[i]; 
							inventory[i] = draggedItem; 
							draggingItem = false; 
							draggedItem = null; 


						}

					}

				} else {
					if(slotRect.Contains(e.mousePosition)){
						if (e.type == EventType.MouseUp && draggingItem){
							inventory[prevIndex] = inventory[i]; 
							inventory[i] = draggedItem; 
							draggingItem = false; 
							draggedItem = null; 
							
						}
						
					}
				}
				if (tooltip == ""){
					showToolTip = false;
				}

				i++; 
			
		}
	}
	GUI.EndGroup ();
	}
	string CreateToolTip(Item item){
		tooltip = "<color=#FFFFFFFF>"+item.itemName+"</color>\n"+ item.itemDesc;
		return tooltip;  

	}
	public void addItem(int id){
		for (int i = 0; i< inventory.Count; i++){
			if (inventory[i].itemName == null){
				for (int j = 0; j < database.items.Count; j++){
					if (database.items[j].itemID == id){
						inventory[i] = database.items[j];
					}
				}
				break;
			}
		}

	}
	void RemoveItem(int id){
		for (int i = 0; i <inventory.Count; i++){
			if (inventory[i].itemID == id){
				inventory[i]= new Item();
				break;
			}
		}
	}
	bool InventoryContains(int id){
		bool result = false; 
		for (int i = 0; i < inventory.Count; i++){
			result =  true;
			if (result){
				break;
			} 			
		}	
		return result; 
	}

	
}





