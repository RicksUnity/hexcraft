using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBlocked : MonoBehaviour {
	public static bool pathBlocked = false;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay (Collider other){
		pathBlocked = true;
	}
}
