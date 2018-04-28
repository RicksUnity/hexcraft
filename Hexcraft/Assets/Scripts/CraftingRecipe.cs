using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Crafting recipes using, when combination matches an item is crafted. 
// Uses list indexing.

public class CraftingRecipe: MonoBehaviour{
	public Dictionary<int, int[]> craftingRecipe = new Dictionary<int, int[]>();
	void Start (){

	//pickaxes
		craftingRecipe.Add(6, new int[] {3,3,3,0,20,0,0,20,0}); //diamond pickaxe
		craftingRecipe.Add(7, new int[] {22,22,22,0,20,0,0,20,0});	//Iron pickaxe
		craftingRecipe.Add(8, new int[] {5,5,5,0,20,0,0,20,0});	//Stone pickaxe
		craftingRecipe.Add(9, new int[] {22,22,22,0,20,0,0,20,0});	//Wood pickaxe
	//swords
		craftingRecipe.Add(14, new int[] {0,3,0,0,3,0,0,20,0});	//Diamond sword
		craftingRecipe.Add(15, new int[] {0,22,0,0,22,0,0,20,0}); 	//Iron sword
		craftingRecipe.Add(16, new int[] {0,5,0,0,5,0,0,20,0});	//Stone sword
		craftingRecipe.Add(17, new int[] {0,22,0,0,22,0,0,20,0});	//Wood sword
	//spades
		craftingRecipe.Add(10, new int[] {0,3,0,0,22,0,0,22,0});	//Diamond…'.'.\.\[] spade
		craftingRecipe.Add(11, new int[] {0,21,0,0,20,0,0,20,0});	//Iron Spade
		craftingRecipe.Add(12, new int[] {0,5,0,0,20,0,0,20,0});	//Stone spade
		craftingRecipe.Add(13, new int[] {0,22,0,0,20,0,0,20,0});	//Wood sword
	//torches
		craftingRecipe.Add(18, new int[] {0,2,0,0,20,0,0,20,0});	//Torch
		craftingRecipe.Add(19, new int[] {0,4,0,0,20,0,0,20,0});	//Reststone torch
	//planks
		craftingRecipe.Add(22, new int[] {0,0,0,0,23,0,0,0,0});	//Planks

	}
		
	}


