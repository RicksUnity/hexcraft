using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobOnGround : MonoBehaviour {
	public static bool onGround = false;

	void OnTriggerStay (Collider other){
		onGround = true;
	}
}
