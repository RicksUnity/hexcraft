using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftingRecipe: MonoBehaviour{
	//public List<RecipeItem> craftingItems = new List<RecipeItem>();
	public Dictionary<string, int[]> craftingRecipe = new Dictionary<string, int[]>();
	void Start (){
	//	craftingItems.Add(new RecipeItem("stick",0,0,0,0,1,0,0,1,0));
		craftingRecipe.Add("stick", new int[] {0,2,0,0,1,0,0,0,0});
	}
		
	}


