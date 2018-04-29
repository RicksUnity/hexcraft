using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
public class SaveLoad: MonoBehaviour {



	public static void Save(string worldName) {
		Debug.Log (worldName);
		GameObject[] objs ;
		int i = 1;

		string fileContent="Hex"+ "\n";
		Dictionary <string, Vector3> data = new Dictionary<string, Vector3>();
		objs = GameObject.FindGameObjectsWithTag("Hex");
		foreach(GameObject obj in objs) {
			Vector3 po = obj.transform.position;
			data.Add ("Hex"+i,po);
			i++;
		}

		foreach (object o in data.Values)
		{
			fileContent += o + ";" ;
			Debug.Log(i+"="+o);
			i++;
		}
		Debug.Log (fileContent);

		StreamWriter writer = new StreamWriter("Assets/Store/"+worldName+".txt", false);
		writer.WriteLine(fileContent);
		writer.Close();
	}	

	public static void Load(string file) {

		StreamReader reader = new StreamReader("Assets/Store/"+file+".txt"); 
		Vector3 rePosition;
		int elementCount = 0;
		string line = "";
		string elementToBeLoad = "";
		GameObject world = null;
		List<string> lines = new List<string>();
		List<string> elements = new List<string> (){"Hex","A","B","C"}; //put the new elements in

		while ((line = reader.ReadLine()) != null)
		{
			line = line.Replace ('(',' ');
			line = line.Replace (')',' ');
			line = line.Replace (';',' ');
			lines.Add(line);
		}
		reader.Close();

		//pPrefab = Resources.Load("Assets/World/Blocks/Cube");
		lines.RemoveAt (0);
		foreach (string position in lines){
			if (position == elements [elementCount]) {
				elementToBeLoad = position;				
				continue;
				elementCount++;
			}
			rePosition = ConvertFromString (position);	
			world = GameObject.Instantiate (Resources.Load(elementToBeLoad)as GameObject,rePosition,Quaternion.identity);
			//instantiatedGameObject.transform.SetParent(null);
			Debug.Log ("here");
		}
		GameObject.FindGameObjectWithTag ("UI").SetActive (false);

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
