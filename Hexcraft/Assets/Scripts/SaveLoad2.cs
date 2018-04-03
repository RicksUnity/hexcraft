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

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}
	public int count (string name){
		
		return GameObject.FindGameObjectsWithTag ("stone").Count();
			

	}



	public void Save (string worldName){
		BinaryFormatter bf = new BinaryFormatter ();
		SavedItem data = new SavedItem ();
		//FileStream file = File.Create (Application.persistentDataPath+"/"+worldName+".dat");
		FileStream file = File.Create ("Assets/Store/"+worldName+".dat");
		//Debug.Log("Here's the number"+count("fff"));
		//foreach (Transform T in Field)
		GameObject[] stones = GameObject.FindGameObjectsWithTag("stone");
		foreach (GameObject objs in stones) {
			data.stone.Add (objs.transform.position.x);
			//Debug.Log (objs.transform.position.x);
			data.stone.Add (objs.transform.position.y);
			//Debug.Log (objs.transform.position.y);
			data.stone.Add (objs.transform.position.z);
			//Debug.Log (objs.transform.position.z);
		}
		foreach (GameObject objs in GameObject.FindGameObjectsWithTag("grass")) {
			data.grass.Add (objs.transform.position.x);
			data.grass.Add (objs.transform.position.y);
			data.grass.Add (objs.transform.position.z);
		}
			
		//inventory
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
			//Debug.Log (posotion.ToString("F4"));
		}
		 j = 1;
		 k = 2;
		for (int i = 0; i<grassSet.Count()/3 ; i=i+3) {
			//Debug.Log (value);
			j = i + 1;
			k = i + 2;
			Vector3 posotion = new Vector3 (grassSet [i], grassSet [j], grassSet [k]);
			GameObject loadBlock = GameObject.Instantiate (Resources.Load("grass ground") as GameObject);
			loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
			loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
			loadBlock.transform.position = posotion;
			//Debug.Log (posotion.ToString("F4"));
		}
		
	}



}

