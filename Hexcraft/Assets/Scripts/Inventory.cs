using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class MainMenu : MonoBehaviour {

	public enum Menu {
		MainMenu,
		NewGame,
		LoadGame,
		None
	}

	int i=0;
	public Menu currentMenu;
	public string worldName;
	public string characterName;
	bool IsEscape;


	public static DirectoryInfo dir = new DirectoryInfo("Assets/Store/");
	public static FileInfo[] info = dir.GetFiles ("*.txt");
	//Debug.Log (i);
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("update");
			//GUI.enabled = true;
			if (!IsEscape)
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
		//Debug.Log ("f");

		/*if (Input.GetKeyDown ("escape")) {
         Debug.Log ("Escape0");
         if (!IsEscape)
         IsEscape = true;
         else
         IsEscape = false;
         Debug.Log (IsEscape);
         }*/

		//if (currentMenu == Menu.InGame) {
		if (IsEscape) {
			//GUI.enabled = true;
			//Debug.Log ("Escape1");
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.BeginVertical ();
			GUILayout.FlexibleSpace ();

			if (GUILayout.Button ("Back to the game")) {
				IsEscape = false;
			} else if (GUILayout.Button ("Save the game")) {
				if (EditorUtility.DisplayDialog ("Save the game","Are you sure you want to save the game?","Yes","No"))
					SaveLoad.Save (worldName);

				//Debug.Log (EditorUtility.DisplayDialog ("Save the game","Are you sure you want to save the game?","Yes","No"));
			} else if (GUILayout.Button ("Save the game and quit")) {

				//Debug.Log ('c');
			} else if (GUILayout.Button ("Quit without saving")) {
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


		else if(currentMenu == Menu.MainMenu) {

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

		else if (currentMenu == Menu.NewGame) {

			GUILayout.Box("Name Your Characters");
			//Debug.Log ("LKJDLKJDFLKSDFJLKSDFJ");
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
					currentMenu = Menu.None;
					SaveLoad.Save (worldName);
				}
				//GUI.enabled = false;

				//SceneManager.LoadScene("map",LoadSceneMode.Single);
				//SceneManager.LoadScene("Scene/New Game",LoadSceneMode.Single);
			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel")) {
				currentMenu = Menu.MainMenu;
			}
		}

		else if (currentMenu == Menu.LoadGame) {


			//SaveLoad.Load();
			GUILayout.Box("Select Saved File");
			GUILayout.Space(10);
			//GUILayout.Box("Select Saved File2");


			//if(i<info.Length){
			foreach (FileInfo f in info){
				//Debug.Log (i);
				GUILayout.Space(10);
				if (GUILayout.Button (Path.GetFileNameWithoutExtension (f.ToString ())))
					SaveLoad.Load (f.ToString ());

				i++;
			}
			//}
			//Debug.Log(i);

			//GUILayout.Space(10);
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
