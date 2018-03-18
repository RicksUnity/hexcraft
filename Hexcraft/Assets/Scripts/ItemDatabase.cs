using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour{
    public List<Item> items = new List<Item>();

    void Start (){
        items.Add(new Item("Stick", 0,"Good in a sticky situation",12, 1, Item.ItemType.Weapon));
        items.Add(new Item("EarthBlock", 1,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("Sandblock", 2,"Coarse...",6, 1, Item.ItemType.Block));
        items.Add(new Item("StoneBlock", 3,"Lift with your knees...",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 4,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 5,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 6,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 7,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 8,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 9,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 10,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 11,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 12,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 13,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 14,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 15,"A pile of mud",6, 1, Item.ItemType.Block));
        items.Add(new Item("EarthBlock", 16,"A pile of mud",6, 1, Item.ItemType.Block));
        
    }
}