using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
[Serializable]
	public class SavedItem{
	public List<float> Leaves = new List<float>();   //create the list for every componet in the game
	public List<float> Tree = new List<float>();

	public List<float> Coal = new List<float>();    
	public List<float> Diamond = new List<float>();  
	public List<float> HexTile_grass = new List<float>();
	public List<float> HexTile_Rock = new List<float>();
	public List<float> HexTile_soil = new List<float>();
	public List<float> Metal = new List<float>();
	public List<float> Red = new List<float>();
	public List<float> transparent = new List<float>();

		public float playerPositionX;
		public float playerPositionY;
		public float playerPositionZ;
	public List<int> inventory = new List<int>();

	public SavedItem(){
		
	}
	}


