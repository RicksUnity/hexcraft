using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBlocked : MonoBehaviour {
	public static bool pathBlocked = false;

	void OnTriggerStay (Collider other){
		pathBlocked = true;
	}
}
