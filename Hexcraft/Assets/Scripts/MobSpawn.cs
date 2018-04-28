using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour {
    public float spawnCountdown = 30;
    public GameObject spawnMob;
    private float spawnRadius = 12f;
    private float lightDistance = 14f;
	// Use this for initialization
	void Start () {
        InvokeRepeating("CheckSpawn", 1, spawnCountdown);
	}

    void CheckSpawn()
    {
        print("Cheese Muffin");
        if (GameObject.Find("Directional Light").GetComponent<DayNight>().time >= 0.25 && GameObject.Find("Directional Light").GetComponent<DayNight>().time <= 0.75)
        {
            Collider[] Enemies = Physics.OverlapSphere(transform.position, spawnRadius);
            int enemyCount = 0;
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].gameObject.tag == "Enemy")
                {
                    enemyCount += 1;
                    print(enemyCount);
                    if (enemyCount >= 5)
                    {
                        break;
                    }
                }
            }
            if (enemyCount < 5)
            {
                for (int i = 0; i < Enemies.Length; i++)
                {
                    GameObject spawnPos = Enemies[Random.Range(0, Enemies.Length)].gameObject;
                    if (AboveCheck(spawnPos))
                    {
                        if(TorchCheck(spawnPos))
                        {
                            GameObject newMob = Instantiate(spawnMob);
                            newMob.transform.position = spawnPos.transform.position + new Vector3(0, 2, 0);
                            return;
                        }
                    }
                }
            }
        }
    }
    bool AboveCheck(GameObject spawnPos)
    {
        Collider[] Above = Physics.OverlapSphere(spawnPos.transform.position, 1.5f);
        for (int j = 0; j < Above.Length; j++)
        {
            if (spawnPos.transform.position.x == Above[j].transform.position.x && spawnPos.transform.position.z == Above[j].transform.position.z && spawnPos.transform.position.y < Above[j].transform.position.y)
            {
                return false;
            }
        }
        return true;
    }

    bool TorchCheck(GameObject spawnPos)
    {
        Collider[] lightCheck = Physics.OverlapSphere(spawnPos.transform.position, lightDistance);
        for (int j = 0; j < lightCheck.Length; j++)
        {
            if (lightCheck[j].gameObject.tag == "Light")
            {
                return false;
            }
        }
        return true;
    }
}
