using UnityEngine;
using System.Collections;

[System.Serializable] // You need this part, so that you class can be 'serialized', meaning turned into 1s and 0s. Binary baby!
public class Character {

	public string name;

	public Character () {
		this.name = "";
	}

}
/*public class Blocks{
	public string ID;
	public Vector3 centerPoint;
	public Vector3 sidePoint;

	public Blocks (){
		this.ID = ""; 
		this.centerPoint = new Vector3(0.0f,0.0f,0.0f);
		this.sidePoint = new Vector3(0.0f,0.0f,0.0f);

	}*/

//}
