using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LoadTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	/*	SaveLoad instance = new SaveLoad ();
		foreach (string position in instance.lines){
			instance.rePosition = ConvertFromString (position);	
			instance.world = GameObject.Instantiate (Resources.Load("Cube")as GameObject,instance.rePosition,Quaternion.identity);
			//instantiatedGameObject.transform.SetParent(null);
			Debug.Log ("here");
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		
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
