using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public float currentHealth = 100;
    public RectTransform healthBar;
    private float healCountdown = 1f;
    private float healAmount = 2f;
    public GameObject sun;

    //Calls the  heal function every set amount of time defined by healCountdown
    void Start () {
        InvokeRepeating("Heal", 1, healCountdown);
    }
	
    //Reconfigures the on screen health bar to represent the currentHealth of the player
	void Update () {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        healthBar.anchoredPosition = new Vector2(-(100 - currentHealth)/2,0);
	}

    void Heal ()
    {
        //If tthe player is dead, retunr them to spawn and make it daytime
        if(currentHealth <= 0)
        {
            transform.position = new Vector3(0, 0, 0);
            currentHealth = 100;
            sun.GetComponent<DayNight>().time = 0;
        }

        //Heals the player by a max amount of healAmount up to 100 health
        if (currentHealth >= 100-healAmount)
        {
            currentHealth = 100;
        }
        else
        {
            currentHealth += healAmount;
        }
    }
}
