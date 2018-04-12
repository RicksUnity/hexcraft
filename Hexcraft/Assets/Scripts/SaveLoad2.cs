using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
public class SaveLoad2: MonoBehaviour {

	//Collision collision;
	public static SaveLoad2 saveload;
	public List<float> stoneSet = new List<float>();
	public List<float> grassSet = new List<float>();
	//public GameObject inventory;
	private Inventory inventory;


	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}
	public int count (string name){
		
		return GameObject.FindGameObjectsWithTag ("stone").Count();
			

	}



	public void Save (string worldName){
		BinaryFormatter bf = new BinaryFormatter ();
		SavedItem data = new SavedItem ();
		FileStream file = File.Create ("Assets/Store/"+worldName+".dat");
		GameObject[] stones = GameObject.FindGameObjectsWithTag("stone");
		foreach (GameObject objs in stones) {
			data.stone.Add (objs.transform.position.x);
			data.stone.Add (objs.transform.position.y);
			data.stone.Add (objs.transform.position.z);
		}
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("grass")) {
			data.grass.Add (objs.transform.position.x);
			data.grass.Add (objs.transform.position.y);
			data.grass.Add (objs.transform.position.z);
		}
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>(); 
		foreach (Item value in Inventory.inventory )
			data.inventory.Add(value.itemID);

		bf.Serialize (file,data);
		file.Close();
	}



	public void Load(string name) {
		Debug.Log (name);
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (name , FileMode.Open);
		SavedItem dataDe = (SavedItem)bf.Deserialize (file); 
		file.Close();
		stoneSet = dataDe.stone;
		grassSet = dataDe.grass;

		int j = 1 ;
		int k = 2;
		for (int i = 0; i<stoneSet.Count()/3 ; i=i+3) {
			//Debug.Log (value);
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (stoneSet [i], stoneSet [j], stoneSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("stone") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent (GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;
		}
		 j = 1;
		 k = 2;
		for (int i = 0; i<grassSet.Count()/3 ; i=i+3) {
			j = i + 1;
			k = i + 2;
			//inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>(); 

			Vector3 posotion = new Vector3 (grassSet [i], grassSet [j], grassSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("grass ground") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;
			//inventory2.addItem (1);
			//Debug.Log ("hh");

		}
		Inventory inventory2 = new Inventory();
		for (int v = 0; v< dataDe.inventory.Count()-1; v++){
				inventory2.addItem (dataDe.inventory[v]);
				//Debug.Log (dataDe.inventory[v]);
				//inventory.inventory[v].itemID = dataDe.inventory[v];
				Debug.Log ("here"+dataDe.inventory[v]);
			}
	}



}

