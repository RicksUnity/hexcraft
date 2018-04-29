using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// All the items in the game declared here. 
// Creates instances using the item class. 

public class ItemDatabase : MonoBehaviour{
    public List<Item> items = new List<Item>();

    //Block references
    public GameObject EarthBlock;
    public GameObject CoalBlock;
    public GameObject DiamondBlock;
    public GameObject RedstoneBlock;
    public GameObject StoneBlock;
    public GameObject Torch;
    public GameObject RedstoneTorch;
    public GameObject IronBlock;
    public GameObject PlankBlock;
    public GameObject WoodBlock;
    public GameObject RedstoneDust;
    public GameObject GrassBlock;
    public GameObject ItemHolder;

    void Start (){
        
        items.Add(new Item("EarthBlock", 1,"A pile of mud",1, 1, Item.ItemType.Block, EarthBlock));
        items.Add(new Item("CoalBlock", 2, "A rocky description", 1, 1, Item.ItemType.Block, CoalBlock));
        items.Add(new Item("DiamondBlock", 3, "A rocky description", 1, 1, Item.ItemType.Block, DiamondBlock));
        items.Add(new Item("RedstoneBlock", 4, "A rocky description", 1, 1, Item.ItemType.Block, RedstoneBlock));
        items.Add(new Item("StoneBlock", 5, "A rocky description", 1, 1, Item.ItemType.Block, StoneBlock));
        items.Add(new Item("DiamondPickaxe", 6, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("IronPickaxe", 7, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("StonePickaxe", 8, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("WoodPickaxe", 9, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("DiamonSpade", 10, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("IronSpade", 11, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("StoneSpade", 12, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("WoodSpade", 13, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("WoodSword", 14, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("StoneSword", 15, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("IronSword", 16, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("DiamondSword", 17, "A rocky description", 1, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("Torch", 18, "A rocky description", 1, 1, Item.ItemType.Block, Torch));
        items.Add(new Item("RedstoneTorch", 19, "A rocky description", 1, 1, Item.ItemType.Block, RedstoneTorch));
        items.Add(new Item("Stick", 20,"Good in a sticky situation",2, 1, Item.ItemType.Weapon, ItemHolder));
        items.Add(new Item("IronBlock", 21,"Good in a sticky situation",2, 1, Item.ItemType.Block, IronBlock));
        items.Add(new Item("PlankBlock", 22,"Good in a sticky situation",2, 1, Item.ItemType.Block, PlankBlock));
        items.Add(new Item("WoodBlock", 23,"Good in a sticky situation",2, 1, Item.ItemType.Block, WoodBlock));
        items.Add(new Item("RedstoneDust", 24, "Good in a sticky situation", 2, 1, Item.ItemType.Block, RedstoneDust));
        items.Add(new Item("GrassBlock", 25, "Good in a sticky situation", 2, 1, Item.ItemType.Block, GrassBlock));

    }
}