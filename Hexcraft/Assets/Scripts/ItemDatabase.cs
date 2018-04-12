using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour{
    public List<Item> items = new List<Item>();

    void Start (){
<<<<<<< HEAD
        items.Add(new Item("Stick", 0,"Good in a sticky situation",2, 1, Item.ItemType.Weapon,"Stick"));
        items.Add(new Item("EarthBlock", 1,"A pile of mud",1, 1, Item.ItemType.Block,"grass ground(Clone)"));
        items.Add(new Item("BlockTemplate", 2, "A rocky description", 1, 1, Item.ItemType.Block,"stone(Clone)"));
=======
        items.Add(new Item("Stick", 2,"Good in a sticky situation",2, 1, Item.ItemType.Weapon));
        items.Add(new Item("EarthBlock", 1,"A pile of mud",1, 1, Item.ItemType.Block));
        
>>>>>>> SaveLoad2
    }
}