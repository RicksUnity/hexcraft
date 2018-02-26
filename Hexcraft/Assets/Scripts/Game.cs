using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game { //don't need ": Monobehaviour" because we are not attaching it to a game object

	public static Game current;
	public Character world;
	public Character player;
	//public Character blocks;
	//public Blocks blocks;
	/*var population = new Tuple<string, int, int, int, int, int, int>(
		"New York", 7891957, 7781984, 
		7894862, 7071639, 7322564, 8008278);*/

	public Game () {
		world = new Character();
		player = new Character();
		//blocks = new Character();
		//blocks = new Blocks();
	}
	
}
