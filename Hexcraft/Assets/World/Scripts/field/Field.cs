using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field: MonoBehaviour {

	private GameObject currentBlockType;

	public GameObject[] blockTypes;

	public float amp = 10f;
	public float freq = 10f;
	public int gird = 20;

	private float tres = Mathf.Sqrt (3);

	private Vector3 myPos;



	// Use this for initialization
	void Start () {
		generateTerrain ();

	}

	void generateTerrain(){

		myPos = this.transform.position;

		int cols = gird;
		int rows = gird;

		for (int z = 0; z < cols; z++) {

			for (int x = 0; x < rows; x++) {



				float y = Mathf.PerlinNoise 
					((myPos.x + x) / freq, 
						(myPos.z + z) / freq) * amp;

				y = Mathf.Floor (y);


				if (y > amp / 2)
					currentBlockType =
						blockTypes [1];
				else
					currentBlockType =
						blockTypes [0];




				GameObject newBlock =
					GameObject.Instantiate (currentBlockType);

				newBlock.transform.SetParent(transform, false);


				newBlock.transform.position =
					new Vector3 ((myPos.z + z)+((myPos.x + x)*2),
						y,
						(myPos.z + z)*tres);
				

			}

		}
	}
}