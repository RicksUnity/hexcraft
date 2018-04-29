using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field: MonoBehaviour {

	private GameObject currentBlockType;
	public GameObject[] blockTypes;

	public GameObject rockbottom;
	private GameObject soilorrock;

	public float amp = 10f;
	public float freq = 10f;
	public int gird = 20;

	private float tres = Mathf.Sqrt (3);

	private Vector3 myPos;
	private Vector3 scale = new Vector3(100f,75f,100f);
	private Vector3 Bpos;

	private int randsoil;


	void Start () {
		generateTerrain ();
	}

	void generateTerrain(){

		myPos = this.transform.position;

		int cols = gird;
		int rows = gird;

		for (int z = 0; z < cols; z++) {
			for (int x = 0; x < rows; x++) {

				float y = Mathf.PerlinNoise (
					(myPos.x + x) / freq, 
					(myPos.z + z) / freq) * amp;

				y = (Mathf.Floor (y))*(1.5f);

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
					new Vector3 (
								(myPos.z + z)+((myPos.x + x)*2),
								y,
								(myPos.z + z)*tres);

				newBlock.transform.localScale = scale;

				Bpos = newBlock.transform.position;

				randsoil =  Random.Range(2,6);

				for (int i = 1; i < 1000; i++) {

					float yrock = Bpos.y-(i*1.5f);

					if (yrock > (Bpos.y-randsoil))
						soilorrock = blockTypes [0];

					else
						soilorrock = rockbottom;
							
					GameObject newrockBlock =
						GameObject.Instantiate (soilorrock);

					newrockBlock.transform.SetParent(transform, false);

					newrockBlock.transform.position = 
						new Vector3 (Bpos.x,
									 yrock,
									 Bpos.z);

					newrockBlock.transform.localScale = scale;

					if (newrockBlock.transform.position.y < -7.5f)
						break;
					
				}
			}
		}
	}
}