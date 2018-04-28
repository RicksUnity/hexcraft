using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobOnGround : MonoBehaviour {
	public static bool onGround = false;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerStay (Collider other){
		onGround = true;
	}
}
