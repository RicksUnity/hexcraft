using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftingRecipe: MonoBehaviour{
	//public List<RecipeItem> craftingItems = new List<RecipeItem>();
	public Dictionary<int, int[]> craftingRecipe = new Dictionary<int, int[]>();
	void Start (){
	//	craftingItems.Add(new RecipeItem("stick",0,0,0,0,1,0,0,1,0));
		craftingRecipe.Add(2, new int[] {0,1,0,0,1,0,0,0,0});
	}
		
	}


