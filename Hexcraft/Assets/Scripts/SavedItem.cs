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
		//public string stone;
	public List<float> stone = new List<float>();
		//public Hex[] stone ;
	public List<float> grass = new List<float>();
		//public string grass;
		public float playerPositionX;
		public float playerPositionY;
		public float playerPositionZ;
		public int[] Inventory;

	public SavedItem(){
		
	}
	}
/*	public class Hex{
	float positionX;
	float positionY;

	public Hex(float positionX, float posisionY){
		positionX = 0;
		positionY = 0;
	}*/
//}

