using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftingSystem:MonoBehaviour{

	public bool isCrafting;
	public string CurrentCraftID;
	public int currentID;
	public Image Result;
	public Sprite EmptySlot;
	public List<Item> items = new List<Item> ();
	public List<CraftableItem> CraftableItems = new List<CraftableItem> ();
	public List<InputField> Craftslots = new List<InputField> ();
	public List<Image> CraftslotsIMG = new List<Image> ();

	void start(){
	}	
	void update(){
		
	}

  public void GetCraftID(){
	CurrentCraftID = "";
	for (int i = 0; i < 9; i ++){
			if (Craftslots [i].text != "") {
				CurrentCraftID += Craftslots [i].text;
				CraftslotsIMG [i].sprite = items [int.Parse (Craftslots [i].text)].img;
			} 
			else {
				CurrentCraftID += "E";	
				CraftslotsIMG [i].sprite = EmptySlot;
			}
	}
		GetItemID (CurrentCraftID);	
}
	public void GetItemID(string CraftID){
		for(int i = 0; i < CraftableItems.Count; i ++){
			if (CraftableItems [i].CraftID == CraftID) {
				currentID = CraftableItems [i].itemID; 
				i = CraftableItems.Count;
				Result.sprite = items [currentID].img;				
			} 
			else {
				currentID = - 1;
				Result.sprite = EmptySlot;
			}
			
		}
		
	}
[System.Serializable]
public class Item{
	public string name;
	public Sprite img;
}
[System.Serializable]
public class CraftableItem{
	public string name;
	public int itemID;
	public string CraftID;

}
}