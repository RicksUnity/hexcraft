using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateItem : MonoBehaviour {
	// Use this for initialization

	Animator useItem; 
	void Start () {
		useItem = GetComponent<Animator>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H))
		{
			
		}
		
	}
}
