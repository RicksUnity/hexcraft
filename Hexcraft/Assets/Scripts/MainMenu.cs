﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
public class MainMenu : MonoBehaviour {

	public enum Menu {
		MainMenu,
		NewGame,
		LoadGame
	}
	int i=0;
	public Menu currentMenu;
	public string worldName;
	public string characterName;
	bool IsEscape;
	void OnGUI () {
		if (Input.GetKeyDown ("escape")) {
			if (!IsEscape)
				IsEscape = true;
			else
				IsEscape = false;
			Debug.Log (IsEscape);
		}
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

				SaveLoad.Save (worldName);
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
		//Cursor.visible = true;
		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		if(currentMenu == Menu.MainMenu) {

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
			Debug.Log ("LKJDLKJDFLKSDFJLKSDFJ");
			Game.current.player.name = GUILayout.TextField(Game.current.player.name, 20);
			GUILayout.Space(10);

			GUILayout.Box("Name your world");
			Game.current.world.name = GUILayout.TextField(Game.current.world.name, 20);
			worldName = Game.current.world.name;
			GUILayout.Space(10);

			if(GUILayout.Button("Create the world!")) {
				SaveLoad.Save(worldName);
				this.enabled = false;
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
			DirectoryInfo dir = new DirectoryInfo("Assets/Store/");
			FileInfo[] info = dir.GetFiles("*.txt*");
			Debug.Log (i);
			foreach (FileInfo f in info)
			{
				Debug.Log(f.ToString());
			}
			i++;

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
