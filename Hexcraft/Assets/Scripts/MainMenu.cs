using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class MainMenu : MonoBehaviour {
	public GameObject Player;
	public enum Menu {
		MainMenu,
		NewGame,
		LoadGame,
		None
	}
	void Start()
	{
		GameObject loadBlock = GameObject.Instantiate (Resources.Load("grass ground") as GameObject);
		loadBlock.transform.parent = GameObject.FindGameObjectWithTag ("LoadGame").transform;
		loadBlock.transform.SetParent(GameObject.FindGameObjectWithTag ("LoadGame").transform,false);
		loadBlock.transform.position = new Vector3(20.0f, 15.79f, 20.0f);
		//transform.position = 
		Vector3 myPos = GameObject.FindGameObjectWithTag ("LoadGame").transform.position;
	}
	/*void start(string name){
		saveload.Load (name);
	} */
	//
	public GameObject field1; 
	int i=0;
	public Menu currentMenu;
	public string worldName;
	public string characterName;
	bool IsEscape;
	public bool Isload;
	SaveLoad2 saveload = new SaveLoad2 ();
	//Field field = new Field (); 
	public static DirectoryInfo dir = new DirectoryInfo("Assets/Store/");
	public static FileInfo[] info = dir.GetFiles ("*.dat");
	//Debug.Log (i);
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("update");
			//GUI.enabled = true;
			if (!IsEscape)      //for in game menu
				IsEscape = true;
			else
				IsEscape = false;
		}

	}

	void OnGUI () {
		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
			
		if (IsEscape) {    //draw the in game menu is press esc
			
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.BeginVertical ();
			GUILayout.FlexibleSpace ();

			if (GUILayout.Button ("Back to the game")) {    //cancel the menu
				IsEscape = false;
			} else if (GUILayout.Button ("Save the game")) {
				if (EditorUtility.DisplayDialog ("Save the game", "Are you sure you want to save the game?", "Yes", "No")) {
					saveload.Save (worldName);     //save the game
					IsEscape = false;
				}
					
				;
			} else if (GUILayout.Button ("Save the game and quit")) {   //save game option and quit
				saveload.Save (worldName); 
				Application.Quit (); 

				//Debug.Log ('c');
			} else if (GUILayout.Button ("Quit without saving")) {   //quit game
				//Debug.Log ('d');
				Application.Quit ();
			}

			GUILayout.FlexibleSpace ();
			GUILayout.EndVertical ();
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();
		}
		//}
		//Cursor.visible = true;

		else if(currentMenu == Menu.MainMenu) {   //draw the main menu

			if(GUILayout.Button("New Game")) {
				//Game.current = new Game();
				currentMenu = Menu.NewGame;
			}
			if(GUILayout.Button("Load Game")) {
				//SaveLoad.Load();
				//SaveLoad.Awake();
				currentMenu = Menu.LoadGame;
			}
			if(GUILayout.Button("Quit")) {
				Application.Quit();
			}
		}

		else if (currentMenu == Menu.NewGame) {    //draw the new game menu

			GUILayout.Box("Name Your Characters");
			characterName = GUILayout.TextField(characterName, 20);
			GUILayout.Space(10);

			GUILayout.Box("Name your world");
			worldName = GUILayout.TextField(worldName, 20);
			//worldName = Game.current.world.name;
			GUILayout.Space(10);

			if(GUILayout.Button("Create the world!")) {
				if (characterName.Length == 0 || worldName.Length == 0)
					EditorUtility.DisplayDialog ("Empty name!", "Character's or world's name can't be empty", "OK");
				else {
					
					currentMenu = Menu.None;	    //save the world
					GameObject field2 = GameObject.Instantiate (field1);   //generate the world
					Vector3 fieldpos  =  new Vector3 (0,0,0);
					field2.transform.position = fieldpos;
					Player.transform.position = new Vector3 (0, 40, 0);   
					saveload.Save (worldName);
				}

			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel")) {
				currentMenu = Menu.MainMenu;
			}
		}

		else if (currentMenu == Menu.LoadGame) {    //draw the load game option



			GUILayout.Box("Select Saved File");
			GUILayout.Space(10);
		
			foreach (FileInfo f in info){
				
			//	GUILayout.Space(10);
				if (GUILayout.Button (Path.GetFileNameWithoutExtension (f.ToString ()))) {
					//if(GUILayout.Button("Create the world!")){
					saveload.Load (f.ToString ());
					Debug.Log("Load the game");
					currentMenu = Menu.None;
					//Player.transform.position = new Vector3 (0, 40, 0);
				}

				i++;
			}

			if(GUILayout.Button("Cancel")) {
				currentMenu = Menu.MainMenu;
			}		
		}

		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}
}
