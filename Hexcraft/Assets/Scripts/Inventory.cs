using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Inventory : MonoBehaviour {
	public int slotsX, slotsY;
	public GUISkin skin;
	public static List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>(); 
	public List<Item> favSlots = new List<Item>(); 
	private bool showInventory;
	private bool showFavourites;    
	private ItemDatabase database; 
	private bool showToolTip; 
	private string tooltip; 
	private bool draggingItem; 
	private Item draggedItem; 
	private int prevIndex; 
	public List<Item> Craftslots = new List<Item> ();
	public List<Item> Craftinventory = new List<Item>();
	private CraftingRecipe Recipe;
	public Rect finishBox = new Rect();
	void Start() {
		for (int i = 0; i < (slotsX*slotsY); i++)
		{
			slots.Add(new Item());
<<<<<<< HEAD
			inventory.Add(new Item());
			favSlots.Add(new Item());
		}
		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>(); 
		addItem(0);
		addItem(1);
=======
			inventory.Add (new Item());
			Craftslots.Add (new Item ());
			Craftinventory.Add (new Item());
		}
		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
		Recipe = GameObject.FindGameObjectWithTag("Crafting Recipe").GetComponent<CraftingRecipe>();

		addItem(1);
		addItem(1);
		//addCraftingItem (1);
		//addCraftingItem (1);
>>>>>>> SaveLoad2
	}
	void Update()
	{
		showFavourites = !showInventory; 
		if(Input.GetButtonDown("Inventory"))
		{
			showInventory = !showInventory;
			showFavourites = !showInventory; 
			
		}
	}
<<<<<<< HEAD
	void OnGUI()
	{
=======

	void OnGUI(){
		//Debug.Log (inventory[0].itemID);
>>>>>>> SaveLoad2
		tooltip = "";
		GUI.skin = skin;  
		if(showInventory)
		{
			showFavourites = true; 
			DrawInventory();
			if (showToolTip)
			{
			GUI.Box (new Rect(Event.current.mousePosition.x +20f, Event.current.mousePosition.y + 20f, 200, 100), tooltip, skin.GetStyle("ToolTip"));
		}
		if (draggingItem)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x +5f, Event.current.mousePosition.y + 5f, 50, 50), draggedItem.itemIcon);

		}
		
		} if(showFavourites)
		{
			DrawFavourites();
		}
		
		}
	
	void DrawInventory()
	{
		Event e = Event.current; 
		int i = 0; 
<<<<<<< HEAD
		GUI.BeginGroup (new Rect (Screen.width / 2 - 250, Screen.height / 3 - 50, 600, 250));
		GUI.Box(new Rect(0,0,600,250), "\n<color=#0>Glorious Inventory!</color>", skin.GetStyle("Background"));
		for (int y = 1; y < slotsY+1; y ++)
		{
			for (int x = 1; x < slotsX+1; x++)
			{
=======
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
>>>>>>> SaveLoad2
				Rect slotRect = new Rect(x * 50, y * 50, 50, 50); 
				Rect craftBox = new Rect(x2 + 35, y2 * 50, 50 ,50);

				GUI.Box(new Rect(slotRect), "", skin.GetStyle("Slot"));
				if (y2 < 4) {
					GUI.Box (new Rect (craftBox), "", skin.GetStyle ("Slot"));
					Craftslots [i] = Craftinventory [i];
				}

				slots[i] = inventory[i];
<<<<<<< HEAD
				if (slots[i].itemName != null)
				{
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					if (slotRect.Contains(e.mousePosition))
					{
=======

				if (slots[i].itemName != null||Craftslots[i].itemName!=null){
					GUI.DrawTexture (craftBox, Craftslots[i].itemIcon);
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					if (slotRect.Contains(e.mousePosition)||craftBox.Contains(e.mousePosition)){
>>>>>>> SaveLoad2
						tooltip = CreateToolTip(slots[i]);
						showToolTip = true; 
						if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
						{
							draggingItem = true;
							prevIndex = i; 
							if (craftBox.Contains (e.mousePosition)) {
								draggedItem = Craftslots [i];
								Craftinventory [i] = new Item ();
							}
							
							else {
								draggedItem = slots [i]; 
								inventory [i] = new Item ();
							}				 
						}
						
<<<<<<< HEAD
						if (e.type == EventType.MouseUp && draggingItem)
						{
							inventory[prevIndex] = inventory[i]; 
							inventory[i] = draggedItem; 
=======
						if (e.type == EventType.MouseUp && draggingItem){							 
							if (slotRect.Contains (e.mousePosition)) {
								inventory [prevIndex] = inventory [i];
								inventory [i] = draggedItem;
							} else {
								Craftinventory [prevIndex] = Craftinventory [i];
								Craftinventory [i] = draggedItem;
							}
>>>>>>> SaveLoad2
							draggingItem = false; 
							draggedItem = null; 
						}

					}

				} else {
<<<<<<< HEAD
					if(slotRect.Contains(e.mousePosition))
					{
						if (e.type == EventType.MouseUp && draggingItem)
						{
							inventory[prevIndex] = inventory[i]; 
							inventory[i] = draggedItem; 
=======
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
>>>>>>> SaveLoad2
							draggingItem = false; 
							draggedItem = null; 
							
						}
						
					}
				}
				if (tooltip == "")
				{
					showToolTip = false;
				}

				i++; 
		}
			//Debug.Log ("gg"+GetCraftID(Craftinventory));
			//GUI.DrawTexture (finishBox, GetCraftID(Craftinventory));

	}
		finishBox = new Rect (x2+75,50,50,50);
		GUI.Box (new Rect (finishBox), "", skin.GetStyle ("Slot"));
		for (int k = 0; k < database.items.Count; k++){
			if (database.items[k].itemID == GetCraftID(Craftinventory)){
				GUI.DrawTexture (finishBox, database.items[k].itemIcon );
				//Craftinventory[i] = database.items[j];
			}
		}
	GUI.EndGroup ();
	}
	void DrawFavourites()
	{

	int i = 0; 
	for( int x = 1; x < 11; x++ )
	{
		Rect favSlotRect = new Rect(x*50, 50, 50, 50);
		GUI.Box(new Rect(favSlotRect), "", skin.GetStyle("Slot"));
		favSlots[i] = inventory[i]; 

		if(favSlots[i].itemName != null)
		{
			GUI.DrawTexture(favSlotRect, favSlots[i].itemIcon);
		}


		i ++;
	}

	}
	
	string CreateToolTip(Item item)
	{
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
<<<<<<< HEAD
	public void RemoveItem(int id){
=======




	void RemoveItem(int id){
>>>>>>> SaveLoad2
		for (int i = 0; i <inventory.Count; i++){
			if (inventory[i].itemID == id){
				inventory[i]= new Item();
				break;
			}
		}
	}
	public bool InventoryContains(int id){
		bool result = false; 
		for (int i = 0; i < inventory.Count; i++){
            if (inventory[i].itemID == id)
            {
                result = true;
                break;
            }		
		}	
		return result; 
	}

	int GetCraftID(List<Item> Craftinventory){
		foreach (KeyValuePair<int,int[]> items in Recipe.craftingRecipe) {
			List<int> tempRecipe = new List<int> ();
			List<int> tempCraftinventory = new List<int> ();
			foreach (int values in items.Value) 				
				tempRecipe.Add (values);
			
			for (int v = 0; v < Craftinventory.Count; v++) {
				tempCraftinventory.Add (Craftinventory [v].itemID);
				//Debug.Log (Craftinventory [v].itemID);
			}
			tempCraftinventory.RemoveRange (9, tempCraftinventory.Count-9);
			//Debug.Log(tempCraftinventory.Count+"//////"+tempRecipe.Count);
			//foreach (int ttt in tempCraftinventory)
				//Debug.Log (ttt);

			if (tempCraftinventory.SequenceEqual(tempRecipe)) 
				return items.Key;
			else
				return 0;
		}
		return 0;
	}
}





