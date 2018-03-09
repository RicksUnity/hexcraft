using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {

	public GameObject playerPrefab;
	bool IsEscape;
	public MainMenu Main;
	// Use this for initialization
	void Start () {
		/*for(int i = 0; i < 3; i++){
			GameObject newDude = Instantiate(playerPrefab, Vector3.right * i * 2, Quaternion.identity) as GameObject;
			if(i==0){
				newDude.name  = Game.current.world.name;
			}
			else if(i==1){
				newDude.name  = Game.current.player.name;
			}
			else if(i==2){
				newDude.name  = Game.current.blocks.name;
			}
		}*/
	}
	void Update (){
		if (Input.GetKeyDown ("escape")) {
			if (!IsEscape)
				IsEscape = true;
			else
				IsEscape = false;
			Debug.Log (IsEscape);
		}
	}
		void OnGUI(){

		if (IsEscape) {
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.BeginVertical ();
			GUILayout.FlexibleSpace ();

			if (GUILayout.Button ("Back to the game")) {
				IsEscape = false;
			} 
			else if (GUILayout.Button ("Save the game")) {
				SaveLoad.Save (Main.worldName);
				Debug.Log ('b');
			} 
			else if (GUILayout.Button ("Save the game and quit")) {
				
				//Debug.Log ('c');
			} 
			else if (GUILayout.Button ("Quit without saving")) {
				//Debug.Log ('d');
				Application.Quit();
			}

			GUILayout.FlexibleSpace ();
			GUILayout.EndVertical ();
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();
		}
	}
		//InGameMenu.DoGUI ();
	}

