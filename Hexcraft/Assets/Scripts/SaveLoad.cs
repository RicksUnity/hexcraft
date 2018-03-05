using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
public class SaveLoad: MonoBehaviour {

	//public static List<Game> savedGames = new List<Game>();

	//it's static so we can call it from anywhere
	public static void Save() {
		/*SaveLoad.savedGames.Add(Game.current);
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
		bf.Serialize(file, SaveLoad.savedGames);
		file.Close();*/
		//Hex = GameObject.FindWithTag ("Hex");
		GameObject[] objs ;
		int i = 1;
		string fileContent="Hex"+ "\n";
		Dictionary <string, Vector3> data = new Dictionary<string, Vector3>();
		objs = GameObject.FindGameObjectsWithTag("Hex" );
		foreach(GameObject obj in objs) {
			Vector3 po = obj.transform.position;
			data.Add ("Hex"+i,po);
			i++;
		}
		//string json = JsonUtility.ToJson(data);

		foreach (object o in data.Values)
			{
			fileContent += o + "," ;
				Debug.Log(i+"="+o);
				i++;
			}
		Debug.Log (fileContent);

		StreamWriter writer = new StreamWriter("Assets/Store/aa.txt", false);
		writer.WriteLine(fileContent);
		writer.Close();
	}	
	
	public static void Load() {
		/*if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
			file.Close();
		}*/
	
		StreamReader reader = new StreamReader("Assets/Store/aa.txt"); 
		Vector3 rePosition;
		string line = "";
		GameObject world = null;
		List<string> lines = new List<string>();
		while ((line = reader.ReadLine()) != null)
		{
			
			lines.Add(line);
		}
		reader.Close();
		if (lines[0]== "Hex" )
			//pPrefab = Resources.Load("Assets/World/Blocks/Cube");
		lines.RemoveAt (0);
		foreach (string position in lines){
			rePosition = ConvertFromString (position);	
			world = GameObject.Instantiate (Resources.Load("Cube")as GameObject,rePosition,Quaternion.identity);
			//instantiatedGameObject.transform.SetParent(null);
			Debug.Log ("here");
		}
		Scene sceneToLoad = SceneManager.GetSceneByName("LoadTest");
		//SceneManager.LoadScene ("LoadTest");
		SceneManager.MoveGameObjectToScene(world, sceneToLoad);
		//SceneManager.SetActiveScene(SceneManager.GetSceneByName("LoadTest"));

		//DontDestroyOnLoad (world);
	}

	static Vector3 ConvertFromString(string input)
	{
		input = input.Remove (0,1);
		input = input.Remove (input.Length-1,1);
		string[] vals = input.Split (',').Select (s => s.Trim ()).ToArray ();

		float v1 = float.Parse (vals[0],System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
		float v2 = float.Parse(vals[1],System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
		float v3 = float.Parse(vals[2],System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
		return new Vector3 (v1, v2, v3);
	}

}
