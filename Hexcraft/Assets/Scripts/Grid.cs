using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid: MonoBehaviour {

	public Transform pointPrefab;

	public int box_x2 = 20;



	void Awake () {

		Vector3 scale = new Vector3(100f,100f,100f);
		Vector3 position;
		position.y = 0f;
		float tres = Mathf.Sqrt (3);


		for (int j = -box_x2; j < box_x2; j++) {
			for (int i = -box_x2; i < box_x2; i++) {
				Transform point = Instantiate(pointPrefab);
				position.x = (j+i*2f);
				position.z = (j*(1f*tres));
				point.localPosition = position;
				point.localScale = scale;
				point.SetParent(transform, false);

			}
		}



	}
}