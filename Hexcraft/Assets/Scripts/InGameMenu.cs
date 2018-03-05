using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	public void DoGUI () {

		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

			if(GUILayout.Button("Back to the game")) {
			Debug.Log ('a');
		}
		else if(GUILayout.Button("Save the game")) {
			Debug.Log ('b');
		}
		else if(GUILayout.Button("Save the game and quit")) {
			Debug.Log ('c');
		}
		else if(GUILayout.Button("Quit without saving")) {
			Debug.Log ('d');
		}



		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}
}
