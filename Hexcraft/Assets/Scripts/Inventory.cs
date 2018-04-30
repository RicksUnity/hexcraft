using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Inventory : MonoBehaviour {
	public GameObject FPC;
	public GUITexture gt;
	public int slotsX, slotsY;
	public GUISkin skin;
	public static List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>(); 
	public List<Item> favSlots = new List<Item>(); 
	public List<Item> selectedSlot = new List<Item>(); 
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
	public List<Item> Finishslots = new List<Item> ();   //new
	public List<Item> Finishinventory = new List<Item>();  //new
	private CraftingRecipe Recipe;
	public Rect finishBox = new Rect();

	int currentitem = 0;

    public enum draggingFrom
	{
		slots, craft, finish, none
	} ;
	draggingFrom drag = draggingFrom.none;
	void Start() {
		 Screen.lockCursor = true;

		gt = GetComponent<GUITexture>();
		for (int i = 0; i < (slotsX*slotsY); i++)
		{
			selectedSlot.Add(new Item()); 
			slots.Add(new Item());
			inventory.Add (new Item());
			Craftslots.Add (new Item ());
			Craftinventory.Add (new Item());
		}
		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
		Recipe = GameObject.FindGameObjectWithTag("Crafting Recipe").GetComponent<CraftingRecipe>();

		addItem(1);
        addItem(18);
        addItem(19);
        addItem(24);
	}
	// Checks for input from the player, if the 'I' key has been pressed opens inventory.
	void Update()
	{
		showFavourites = !showInventory; 
		if(Input.GetButtonDown("Inventory"))
		{
			showInventory = !showInventory;
			showFavourites = !showInventory; 
			print(Screen.lockCursor);
			Screen.lockCursor = !Screen.lockCursor;
			print(Screen.lockCursor);
			//FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().MouseLook.m_cursorIsLocked = false;
		}
        
        if (!Screen.lockCursor && wasLocked) {
            wasLocked = false;
            DidUnlockCursor();
        } else
            if (Screen.lockCursor && !wasLocked) {
                wasLocked = true;
                DidLockCursor();

		}
	}
// Used to draw the inventory menu.
//Used to draw the favourites (first ten slots of the inventory).
	void OnGUI(){
		//Debug.Log (inventory[0].itemID);
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
		
		}
		if(showFavourites)
		{
			DrawFavourites();
		}
		
		}
// Draw when conditions are met. 
// Contains the logic for dragging items from and to different slots
// as well as to the creafting menu. 

	void DrawInventory()
	{
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
				GUI.Box(new Rect(slotRect), "", skin.GetStyle("SelectedSlot"));

				if (y2 < 4) {
					GUI.Box (new Rect (craftBox), "", skin.GetStyle ("Slot"));
					Craftslots [i] = Craftinventory [i];
				}

				slots[i] = inventory[i];

				if (slots[i].itemName != null||Craftslots[i].itemName!=null){
					GUI.DrawTexture (craftBox, Craftslots[i].itemIcon);
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					if (slotRect.Contains(e.mousePosition)||craftBox.Contains(e.mousePosition)||finishBox.Contains (e.mousePosition)){
						tooltip = CreateToolTip(slots[i]);
						showToolTip = true; 
						if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
						{
							
							prevIndex = i;
							if (craftBox.Contains (e.mousePosition)) {
								draggedItem = Craftslots [i];
								Craftinventory [i] = new Item ();
								Debug.Log ("craft drag:"+i);
							}
							else if (finishBox.Contains (e.mousePosition)) {
								draggedItem = Finishslots [0];
								Finishinventory [0] = new Item ();
								//drag = draggingFrom.finish;
								for (int s = 0; s < Craftinventory.Count; s++)
									Craftinventory [s] = new Item();
								
							}

							else if(slotRect.Contains(e.mousePosition)){
								draggedItem = slots [i];
								inventory [i] = new Item ();

							}
							draggingItem = true;
						}

						if (e.type == EventType.MouseUp && draggingItem){
							if (slotRect.Contains (e.mousePosition)) {
								inventory [prevIndex] = inventory [i];
								inventory [i] = draggedItem;

							}
							else if (finishBox.Contains (e.mousePosition)) {
								Finishinventory [i] = draggedItem;


							}
							else  {
								Craftinventory [i] = draggedItem;
							}
							draggingItem = false;
							draggedItem = null;
						}

					}

				}  else {
					if(slotRect.Contains(e.mousePosition)||craftBox.Contains(e.mousePosition)||finishBox.Contains (e.mousePosition)){
						if (e.type == EventType.MouseUp && draggingItem){
							if (slotRect.Contains (e.mousePosition)) {
								//inventory [prevIndex] = inventory [i];
								inventory [i] = draggedItem;
								Debug.Log ("inventory else mouse up:"+i);
								//if (drag == draggingFrom.finish)
								//	Finishinventory [0] = null;
							}
							else if (finishBox.Contains (e.mousePosition)) {
								//Finishinventory [0] = Finishinventory [0];
								Finishinventory [i] = draggedItem;
								Debug.Log ("finish else mouse up:" + i);
							}
							else if(craftBox.Contains (e.mousePosition)) {
								//Craftinventory [prevIndex] = Craftinventory [i];
								Craftinventory [i] = draggedItem;
								Debug.Log ("craft else mouse up:"+i);
							}
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
		finishBox = new Rect (x2+200,100,50,50);
		GUI.Box (new Rect (finishBox), "", skin.GetStyle ("Slot"));
		for (int k = 0; k < database.items.Count; k++){
			if (database.items[k].itemID == GetCraftID(Craftinventory)){
				Finishinventory.Insert(0,database.items[k]) ;
				GUI.DrawTexture (finishBox, database.items[k].itemIcon );
				Finishslots.Insert(0,database.items[k]);
				//public List<Item> Craftslots = new List<Item> ();
				//public List<Item> Craftinventory = new List<Item>();
				//Craftinventory[i] = database.items[j];
			}
		}
		GUI.EndGroup ();
	}

// Draws favourites on to the screen in the top left corner
// if slot empty, draws an empty slot. 
	void DrawFavourites()
	{
		
		favSlots.Clear();
		int i = 0;
		for( int x = 1; x < 11; x++ )
		{
			//seke
			//Debug.Log ("this is drawFavorite:"+ i);
			Rect favSlotRect = new Rect(x*50, 50, 50, 50);
			
			

			GUI.Box(new Rect(favSlotRect), "", skin.GetStyle("Slot"));
			
			//finishBox = new Rect (50,100,50,50);

			favSlots.Add(inventory[i]); 

			if(favSlots[i].itemName != null)
			{
				
				GUI.DrawTexture(favSlotRect, favSlots[i].itemIcon);
	
			}
			
			//Debug.Log ("fav:"+favSlots[i]+"inventory:"+inventory[i]);

			i++;
		}
		DrawSelected();

	}
	// Draws the item currently selected in a seperate, golden, slot. 
	void DrawSelected(){
		SelectedItem item = new SelectedItem();
		Rect selSlotRect = new Rect(50,100,50,50);
		GUI.Box(new Rect(selSlotRect), "", skin.GetStyle("SelectedItem"));
		print(SelectedItem.selectedItem);
		currentitem = SelectedItem.selectedItem; 
		GUI.DrawTexture(selSlotRect, favSlots[currentitem].itemIcon);
		

	}
// Tooltip appears when hovering above an item, stating its name and description.
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



// Called to remove an item from the inventory. 
	public void RemoveItem(int id){
		for (int i = 0; i <inventory.Count; i++){
			if (inventory[i].itemID == id){
				inventory[i]= new Item();
				break;
			}
		}
	}
// Called to check whether or not an item is in the invetory. 
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
    void DidLockCursor() {
        Debug.Log("Locking cursor");
        gt.enabled = false;
    }
    void DidUnlockCursor() {
        Debug.Log("Unlocking cursor");
        gt.enabled = true;
    }
	private bool wasLocked = false;
}






