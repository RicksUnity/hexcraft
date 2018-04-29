using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field: MonoBehaviour {

private GameObject currentBlockType;
	public GameObject[] blockTypes;
	public GameObject rockbottom;
	private GameObject soilorrock;
	public int grid = 5;	

	float noisescale = 40f;

	int octaves = 1;
	[Range(0,1)]
	float persistance = 0.5f;
	float lacunarity = 1;
	public int seed=0;

	private float tres = Mathf.Sqrt (3);

	private Vector3 myPos;
	private Vector3 scale = new Vector3(100f,100f,100f);
	private Vector3 Bpos;

	private int randsoil;
	private int randtree;

	public GameObject TreeFab;

	void Start () {

		generateTerrain();
		
	}	

	public void generateTerrain(){

		seed = Random.Range(0,100000);
		myPos = this.transform.position;
		
		float offsetx = myPos.x;
		float offsety = myPos.z;

		int cols = grid/2;
		int rows = grid/2;

//------------Noise Generator-----------------------------------------

		System.Random prng = new System.Random (seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++) {
			float offsetX = prng.Next (-100000, 100000) + offsetx;
			float offsetY = prng.Next (-100000, 100000) + offsety;
			octaveOffsets [i] = new Vector2 (offsetX, offsetY);
		}

		if (noisescale <= 0) {
			noisescale = 0.0001f;
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = cols / 2f;
		float halfHeight = rows / 2f;
		
//-------------FOR Sicles----------------------------------------

		for (int z = -cols; z < cols+2; z++) {
			for (int x = -rows; x < rows+1; x++) {
				
//-------------Noise Tuner and block selector----------------------------------------

				float amplitude = 1.5f;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++) {
					float sampleX = (x-halfWidth) / noisescale * frequency + octaveOffsets[i].x;
					float sampleY = (z-halfHeight) / noisescale * frequency + octaveOffsets[i].y;

					float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight) {
					maxNoiseHeight = noiseHeight;
				} else if (noiseHeight < minNoiseHeight) {
					minNoiseHeight = noiseHeight;
				}
				float y = 6+noiseHeight*10;

				y = (Mathf.Floor (y))*(2);

				if (y > 0)
					currentBlockType =
					blockTypes [1];
				
				else
					currentBlockType =
					blockTypes [0];

//-------------Block alighment and plasement---------------------------------------

				GameObject newBlock =
					GameObject.Instantiate (currentBlockType);
				newBlock.transform.SetParent(transform, false);
				newBlock.transform.position =
					new Vector3 (
								(x*2+(0.5f-Mathf.Pow(-1,z)/2))+myPos.x,
								y,
								(z*tres)+myPos.z
								);
				newBlock.transform.localScale = scale;
				Bpos = newBlock.transform.position;
				randsoil =  Random.Range(3,12);
				randtree =  Random.Range(1,40);


				if (Bpos.y >= 2)
					if (Bpos.y <= 24)
						if (randtree == 5)
						Tree(Bpos.x,Bpos.y+1,Bpos.z) ;





				if (newBlock.transform.position.y <= -12)
					Destroy(newBlock);

				for (int i = 1; i < 1000; i++) {

					float yrock = Bpos.y-(i*2);

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

					if (newrockBlock.transform.position.y <= -12)
						Destroy(newrockBlock);

					if (newrockBlock.transform.position.y < -8)
						break;

				}
			}
		}
		



	}

//-------------Tree---------------------------------------
	void Tree(float px,float py,float pz){

			GameObject newTree = GameObject.Instantiate (TreeFab);
			Vector3 treePos = new Vector3(px,py,pz);
			newTree.transform.position = treePos;
	}	

}



