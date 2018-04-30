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
	public List<float> LeavesSet = new List<float>();
	public List<float> TreeSet = new List<float>();



	public List<float> CoalSet = new List<float>();         //Create list for every property
	public List<float> DiamondSet = new List<float>();
	public List<float> HexTile_grassSet = new List<float>();
	public List<float> HexTile_RockSet = new List<float>();
	public List<float> HexTile_soilSet = new List<float>();
	public List<float> MetalSet = new List<float>();
	public List<float> RedSet = new List<float>();
	public List<float> transparentSet = new List<float>();
	//public GameObject Player;



	//public GameObject inventory;
	private Inventory inventory;


	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}
	public int count (string name){
		
		return GameObject.FindGameObjectsWithTag (name).Count();
			

	}



	public void Save (string worldName){
		
		BinaryFormatter bf = new BinaryFormatter ();
		SavedItem data = new SavedItem ();
		FileStream file = File.Create ("Assets/Store/"+worldName+".dat");    //create the file for saving the game
		GameObject[] Leaves = GameObject.FindGameObjectsWithTag("Leaves");
		foreach (GameObject objs in Leaves) {               //save the stone to the game
			data.Leaves.Add (objs.transform.position.x);  
			data.Leaves.Add (objs.transform.position.y);
			data.Leaves.Add (objs.transform.position.z);
			Debug.Log("Save the Leaves"+objs.transform.position.x);
		}
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("Tree")) {//save the grass to the game
			data.Tree.Add (objs.transform.position.x);
			data.Tree.Add (objs.transform.position.y);
			data.Tree.Add (objs.transform.position.z);
		}

		////////////////////
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("coal")) {  //save the coal to the game
			data.Coal.Add (objs.transform.position.x);
			data.Coal.Add (objs.transform.position.y);
			data.Coal.Add (objs.transform.position.z);
		}
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("Diamond")) { //save the diamond to the game
			data.Diamond.Add (objs.transform.position.x);
			data.Diamond.Add (objs.transform.position.y);
			data.Diamond.Add (objs.transform.position.z);
		}
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("HexTile_grass")) {  //save the hextile grass to the game
			data.HexTile_grass.Add (objs.transform.position.x);
			data.HexTile_grass.Add (objs.transform.position.y);
			data.HexTile_grass.Add (objs.transform.position.z);
			Debug.Log ("Save"+objs.transform.position.z);
		}
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("HexTile_Rock")) {   //save the hextile rock to the game
			data.HexTile_Rock.Add (objs.transform.position.x);
			data.HexTile_Rock.Add (objs.transform.position.y);
			data.HexTile_Rock.Add (objs.transform.position.z);
		}
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("HexTile_soil")) {  //save the hextile soil to the game
			data.HexTile_soil.Add (objs.transform.position.x);
			data.HexTile_soil.Add (objs.transform.position.y);
			data.HexTile_soil.Add (objs.transform.position.z);
		}
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("Metal")) {  //save the hextile metal to the game
			data.Metal.Add (objs.transform.position.x);
			data.Metal.Add (objs.transform.position.y);
			data.Metal.Add (objs.transform.position.z);
		}
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("Red")) {  //save the red to the game
			data.Red.Add (objs.transform.position.x);
			data.Red.Add (objs.transform.position.y);
			data.Red.Add (objs.transform.position.z);
		}
		/*foreach (GameObject objs in GameObject.FindGameObjectsWithTag("transparent")) {  //save the transparent to the game
			data.grass.Add (objs.transform.position.x);
			data.grass.Add (objs.transform.position.y);
			data.grass.Add (objs.transform.position.z);
		}*/



		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>(); 
		foreach (Item value in Inventory.inventory )
			data.inventory.Add(value.itemID);

		bf.Serialize (file,data);
		file.Close();
	}



	public void Load(string name) {  //Load the game back 
		Debug.Log ("Load function");
		Debug.Log (name);
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (name , FileMode.Open);  // open the file of the saved game
		SavedItem dataDe = (SavedItem)bf.Deserialize (file);  //deserialize the file
		file.Close();
		LeavesSet = dataDe.Leaves;   //get the data from the saved file
		TreeSet = dataDe.Tree;



		CoalSet = dataDe.Coal;
		DiamondSet = dataDe.Diamond;
		HexTile_grassSet = dataDe.HexTile_grass;
		HexTile_RockSet = dataDe.HexTile_Rock;
		HexTile_soilSet = dataDe.HexTile_soil;
		MetalSet = dataDe.Metal;
		RedSet = dataDe.Red;
		transparentSet = dataDe.transparent;





		int j = 1 ;
		int k = 2;
		for (int i = 0; i<LeavesSet.Count()/3 ; i=i+3) {   //Load the sleaves
		//	Debug.Log ("Leaves"+value);
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (LeavesSet [i], LeavesSet [j], LeavesSet [k]); 
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("Leaves") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent (GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;
			Debug.Log ("Loaging game here"+"Leaves");
			//Debug.Log ("Leaves"+LeavesSet [i]);
		}
		 j = 1;
		 k = 2;
		for (int i = 0; i<TreeSet.Count()/3 ; i=i+3) {    //Load the tree
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (TreeSet [i], TreeSet [j], TreeSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("Tree") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;

		}


		////////////////////////////////////////////////////////
		j = 1;
		k = 2;
		for (int i = 0; i<CoalSet.Count()/3 ; i=i+3) {   //load the coal to the game
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (CoalSet [i], CoalSet [j], CoalSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("coal") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;

		}
		j = 1;
		k = 2;
		for (int i = 0; i<DiamondSet.Count()/3 ; i=i+3) {  //load the diamond to the game
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (DiamondSet [i], DiamondSet [j], DiamondSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("Diamond") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;

		}
		j = 1;
		k = 2;
		for (int i = 0; i<HexTile_grassSet.Count()/3 ; i=i+3) {  //load the hextile grass to the game
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (HexTile_grassSet [i], HexTile_grassSet [j], HexTile_grassSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("HexTile_grass") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;

		}
		j = 1;
		k = 2;
		for (int i = 0; i<HexTile_RockSet.Count()/3 ; i=i+3) {  //load the hextile rock to the game
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (HexTile_RockSet [i], HexTile_RockSet [j], HexTile_RockSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("HexTile_Rock") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;

		}
		j = 1;
		k = 2;
		for (int i = 0; i<HexTile_soilSet.Count()/3 ; i=i+3) {   //load the hextile soil to the game
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (HexTile_soilSet [i], HexTile_soilSet [j], HexTile_soilSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("HexTile_soil") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;

		}
		j = 1;
		k = 2;
		for (int i = 0; i<MetalSet.Count()/3 ; i=i+3) {  //load the metal to the game
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (MetalSet [i], MetalSet [j], MetalSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("Metal") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;

		}
		j = 1;
		k = 2;
		for (int i = 0; i<RedSet.Count()/3 ; i=i+3) {  //load the red to the game
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (RedSet [i], RedSet [j], RedSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("Red") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;

		}
		j = 1;
		k = 2;
		for (int i = 0; i<transparentSet.Count()/3 ; i=i+3) {   //load the transparent to the game
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (transparentSet [i], transparentSet [j], transparentSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("transparent") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;

		}







		//Inventory inventory2 = new Inventory(); 
		GameObject inventory2 = GameObject.Find("Inventory"); 
		Inventory a = (Inventory)inventory2.GetComponent (typeof(Inventory)); 
		for (int v = 0; v< dataDe.inventory.Count()-1; v++){ 
			a.addItem (dataDe.inventory[v]); 
			//inventory2.gameObject.addItem (dataDe.inventory[v]); 
			Debug.Log (dataDe.inventory[v]); 
			//inventory.inventory[v].itemID = dataDe.inventory[v]; 
			Debug.Log ("here"+dataDe.inventory[v]); 
		} 
		//Player.transform.position = new Vector3 (0, 40, 0);





		/*Inventory inventory2 = new Inventory();
		for (int v = 0; v< dataDe.inventory.Count()-1; v++){   //load the inventory
				inventory2.addItem (dataDe.inventory[v]);
				//Debug.Log (dataDe.inventory[v]);
				//inventory.inventory[v].itemID = dataDe.inventory[v];
				Debug.Log ("here"+dataDe.inventory[v]);
			}*/
	}



}

