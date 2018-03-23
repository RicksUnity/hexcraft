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
	public List<Item> Craftslots = new List<Item> ();
	public List<Item> Craftinventory = new List<Item>();
	private CraftingRecipe Recipe;
	public List<int> tempCraftinventory = new List<int> ();
	void Start() {
		for (int i = 0; i < (slotsX*slotsY); i++){
			slots.Add(new Item());
			inventory.Add (new Item());
			Craftslots.Add (new Item ());
			Craftinventory.Add (new Item());
		}
		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
		Recipe = GameObject.FindGameObjectWithTag("Crafting Recipe").GetComponent<CraftingRecipe>();

		//addItem(1);
		//addItem(0);
		addCraftingItem (1);
		addCraftingItem (2);
	}
	void Update(){
		if(Input.GetButtonDown("Inventory")){
			showInventory = !showInventory;
		}
	}
	void OnGUI(){
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
		int x2 = slotsX * 50  ;
		int y2 = 1; 
		GUI.BeginGroup (new Rect (Screen.width / 2 - 400, Screen.height / 3 - 50, 900, 250));
		GUI.Box(new Rect(0,0,900,250), "\n<color=#0>Glorious Inventory!</color>", skin.GetStyle("Background"));
		for (int y = 1; y < slotsY+1; y ++){
			for (int x = 1; x < slotsX+1; x++){
				if (x2 > (slotsX * 50 + 100) ) {
					y2 ++ ; 
					x2 = slotsX * 50; 
				}
				x2 += 50;	
				Rect slotRect = new Rect(x * 50, y * 50, 50, 50); 
				Rect craftBox = new Rect(x2 + 35, y2 * 50, 50 ,50);
				GUI.Box(new Rect(slotRect), "", skin.GetStyle("Slot"));
				if (y2 < 4) {
					GUI.Box (new Rect (craftBox), "", skin.GetStyle ("Slot"));
					Craftslots [i] = Craftinventory [i];
				}
				slots[i] = inventory[i];

				if (slots[i].itemName != null||Craftslots[i].itemName!=null){
					GUI.DrawTexture (craftBox, Craftslots[i].itemIcon);
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					if (slotRect.Contains(e.mousePosition)||craftBox.Contains(e.mousePosition)){
						tooltip = CreateToolTip(slots[i]);
						showToolTip = true; 
						if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem){
							draggingItem = true;
							prevIndex = i; 
							if (craftBox.Contains (e.mousePosition))
								draggedItem = Craftslots [i];
							else
								draggedItem  = slots[i]; 
							inventory[i] = new Item();						 
						}
						
						if (e.type == EventType.MouseUp && draggingItem){							 
							if (slotRect.Contains (e.mousePosition)) {
								inventory [prevIndex] = inventory [i];
								inventory [i] = draggedItem;
							} else {
								Craftinventory [prevIndex] = Craftinventory [i];
								Craftinventory [i] = draggedItem;
							}
							draggingItem = false; 
							draggedItem = null; 
						}

					}

				} else {
					if(slotRect.Contains(e.mousePosition)||craftBox.Contains(e.mousePosition)){
						if (e.type == EventType.MouseUp && draggingItem){
							if (slotRect.Contains (e.mousePosition)) {
								inventory [prevIndex] = inventory [i]; 
								inventory [i] = draggedItem; 
							}
							else {
								Craftinventory [prevIndex] = Craftinventory [i];
								Craftinventory [i] = draggedItem;
							}
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
			/*Debug.Log ("START");
			for (int k = 0; k < Craftinventory.Count; k++)
				Debug.Log ("aaa:   "+Craftinventory[k].itemID);
			Debug.Log ("END");*/
			 
			print (GetCraftID (Craftinventory).ToString());
			Debug.Log ("gg"+GetCraftID(Craftinventory));

	}
	GUI.EndGroup ();
	}
	string CreateToolTip(Item item){
		tooltip = "<color=#FFFFFFFF>"+item.itemName+"</color>\n"+ item.itemDesc;
		return tooltip;  

	}
	void addItem(int id){
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
	void addCraftingItem (int id){
		for (int i = 0; i< Craftinventory.Count; i++){
			if (Craftinventory[i].itemName == null){
				for (int j = 0; j < database.items.Count; j++){
					if (database.items[j].itemID == id){
						Craftinventory[i] = database.items[j];
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

	string GetCraftID(List<Item> Craftinventory){
		
		//Recipe.craftingRecipe.ToList ();
		foreach (KeyValuePair<string,int[]> items in Recipe.craftingRecipe){
			List<int> tempRecipe = new List<int>();

			foreach (int values in items.Value)
				tempRecipe.Add (values);
			for (int v = 0; v < Craftinventory.Count; v++)
				tempCraftinventory.Add (Craftinventory[v].itemID);
			//tempCraftinventory.RemoveRange (9, 29);

			if (tempCraftinventory == tempRecipe) 
				return items.Key;

			else
				return string.Empty;
		}
		return string.Empty;
		//GetItemID (CurrentCraftID);	
	}
}





