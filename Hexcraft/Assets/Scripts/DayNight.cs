using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

    public Light sun;
    public float time;
    public float dayLength = 30;
	// Update is called once per frame
    
	void Update () {
        time += (Time.deltaTime / dayLength);
        if (time >= 1)
            time = 0;
        sun.transform.localRotation = Quaternion.Euler((time * 360f) + 90, 170, 0);
        if (time < 0.5)
            sun.intensity = 1 - 2 * time;
        else
            sun.intensity = 2 * time - 1;
    }
}
