using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {
    public Light sun;
    public float time;
    public float dayLength = 3600;
    
    //Increment time every day
	void Update () {
        time += (Time.deltaTime / dayLength);
        //If time reaches 1 (the end of the day) loop it back to zero.
        if (time >= 1)
            time = 0;
        //Move the sun round the sky as time progresses
        sun.transform.localRotation = Quaternion.Euler((time * 360f) + 90, 170, 0);
        //match the intensity to the time of day of the sun so the sun is briightest in the middle of the day.
        if (time < 0.5)
            sun.intensity = 1 - 2 * time;
        else
            sun.intensity = 2 * time - 1;
    }
}
