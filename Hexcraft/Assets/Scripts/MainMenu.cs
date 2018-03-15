using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
public class MainMenu : MonoBehaviour {

	public enum Menu {
		MainMenu,
		NewGame,
		LoadGame,
		InGame
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
			Debug.Log ("Escape1");
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.BeginVertical ();
			GUILayout.FlexibleSpace ();

			if (GUILayout.Button ("Back to the game")) {
				IsEscape = false;
			} else if (GUILayout.Button ("Save the game")) {

				SaveLoad.Save (worldName);
				Debug.Log ('b');
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
				Game.current = new Game();
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
			Game.current.player.name = GUILayout.TextField(Game.current.player.name, 20);
			GUILayout.Space(10);

			GUILayout.Box("Name your world");
			Game.current.world.name = GUILayout.TextField(Game.current.world.name, 20);
			worldName = Game.current.world.name;
			GUILayout.Space(10);

			if(GUILayout.Button("Create the world!")) {
				currentMenu = Menu.InGame;
				SaveLoad.Save(worldName);
				//this.enabled = false;

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
