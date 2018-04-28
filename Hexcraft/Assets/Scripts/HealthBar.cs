using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public float currentHealth = 100;
    public RectTransform healthBar;
    private float healCountdown = 1f;
    private float healAmount = 2f;
    public GameObject sun;

	void Start () {
        InvokeRepeating("Heal", 1, healCountdown);
    }
	
	// Update is called once per frame
	void Update () {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        healthBar.anchoredPosition = new Vector2(-(100 - currentHealth)/2,0);
	}

    void Heal ()
    {
        if(currentHealth <= 0)
        {
            transform.position = new Vector3(20, 20, 20);
            currentHealth = 100;
            sun.GetComponent<DayNight>().time = 0;
        }
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
