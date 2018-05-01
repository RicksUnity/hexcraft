using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field: MonoBehaviour {



	private GameObject currentBlockType;
	public GameObject[] blockTypes;
	public GameObject rockbottom;
	private GameObject soilorrockormineral;
	public int grid = 5;	

	float noisescale = 40f;

	int octaves = 1;
	[Range(0,1)]
	float persistance = 0.5f;
	float lacunarity = 1;
	private int seed;

	private float tres = Mathf.Sqrt (3);

	private Vector3 myPos;
	private Vector3 scale = new Vector3(100f,100f,100f);
	private Vector3 Bpos;

	private int randsoil;
	private int randtree;
	private int randmineral;

	public GameObject TreeFab;

	private float hex1;
	private float hex2;

	void Start () {

		generateTerrain();

	}	

	public void generateTerrain(){
		//------------initialize vars---------------------------
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

		for (int z = -grid+1; z < grid; z++) {
			hex1=((Mathf.Abs(z))*-1)+(grid);
			for (int x = -grid+1; x < hex1; x++) {

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

				if (y > 0) {
					currentBlockType =
						blockTypes [1];
					tag = "HexTile_grass";
				} else {
					currentBlockType =
						blockTypes [0];
					tag = "HexTile_soil";
				}

				//-------------Block alighment and plasement---------------------------------------

				GameObject newBlock =
					GameObject.Instantiate (currentBlockType);
				newBlock.transform.SetParent(transform, false);

				hex2 = z;

				if(hex2>0){
					hex2=hex2*(-1);
				}

				newBlock.transform.position =
					new Vector3 (
								(x*2+myPos.x-hex2),
								y,
								(z*tres)+myPos.z
								);
				newBlock.transform.localScale = scale;
				Bpos = newBlock.transform.position;



				//--------------Tree pos--------------------------------------

				randtree =  Random.Range(1,45);

				if (Bpos.y >= 2)
				if (Bpos.y <= 24)
				if (randtree == 5)
					Tree(Bpos.x,Bpos.y+1,Bpos.z) ;

				//--------------Minerals and underground--------------------------------------

				randsoil =  Random.Range(3,12);

				for (int i = 1; i < 1000; i++) {
					randmineral =  Random.Range(1,10000);

					float yrock = Bpos.y-(i*2);

					if (yrock > (Bpos.y - randsoil)) {
						soilorrockormineral = blockTypes [0];
						tag = "HexTile_Rock";
					}

					

					else
						soilorrockormineral = rockbottom;

					if (randmineral > 1 & randmineral < 70) {
						soilorrockormineral = blockTypes [2];
						tag = "Diamond";
					}

					if (randmineral > 100 & randmineral < 300) {
						soilorrockormineral = blockTypes [3];
						tag = "coal";
					}

					if (randmineral > 301 & randmineral < 500) {
						soilorrockormineral = blockTypes [4];	
						tag = "Red";
					}

					if (randmineral > 501 & randmineral < 700) {
						soilorrockormineral = blockTypes [5];
						tag = "Metal";
					}


					GameObject newrockBlock =
						GameObject.Instantiate (soilorrockormineral);
					newrockBlock.transform.SetParent(transform, false);
					newrockBlock.tag = tag;
					newrockBlock.transform.position = 
						new Vector3 (Bpos.x,
							yrock,
							Bpos.z);
					newrockBlock.transform.localScale = scale;

					if (newrockBlock.transform.position.y < -16)
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
		newTree.transform.SetParent(transform, false);
	}	

}