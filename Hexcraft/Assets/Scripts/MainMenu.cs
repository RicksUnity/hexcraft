using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

	public enum Menu {
		MainMenu,
		NewGame,
		LoadGame
	}

	public Menu currentMenu;
	public string worldName;
	public string characterName;

	void OnGUI () {
		
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
				SaveLoad.Load();
				//currentMenu = Menu.LoadGame;
			}

			if(GUILayout.Button("Quit")) {
				Application.Quit();
			}
		}

		else if (currentMenu == Menu.NewGame) {

			GUILayout.Box("Name Your Characters");
			Game.current.player.name = GUILayout.TextField(Game.current.player.name, 20);
			GUILayout.Space(10);

			GUILayout.Label("Name your world");
			Game.current.world.name = GUILayout.TextField(Game.current.world.name, 20);


			if(GUILayout.Button("Save")) {
				SaveLoad.Save();
				SceneManager.LoadScene("map",LoadSceneMode.Single);
				//SceneManager.LoadScene("Scene/New Game",LoadSceneMode.Single);
			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel")) {
				currentMenu = Menu.MainMenu;
			}
		}

		else if (currentMenu == Menu.LoadGame) {
			
			GUILayout.Box("Select Save File");
			GUILayout.Space(10);

			// Have to think of a way to do this related to the saved game

			/*foreach(Game g in SaveLoad.savedGames) {
				if(GUILayout.Button(g.knight.name + " - " + g.rogue.name + " - " + g.wizard.name)) {
					Game.current = g;
					//Move on to game...
					//Application.LoadLevel(1);

				}

			}*/

			GUILayout.Space(10);
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
