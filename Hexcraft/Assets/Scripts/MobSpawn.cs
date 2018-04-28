using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour {
    public float spawnCountdown = 30;
    public GameObject spawnMob;
    private float spawnRadius = 12f;
    private float lightDistance = 14f;
    private GameObject Sun = GameObject.Find("Sun");

    // --- CHeck to see if a mob should spawn every set amount of time defined by spawnCountdown
    void Start () {
        InvokeRepeating("CheckSpawn", 1, spawnCountdown);
	}
    // --- CHecks whether a MOB cna spwn then spawns it ---
    void CheckSpawn()
    {
        // Only spawn if it is night
        if (Sun.GetComponent<DayNight>().time >= 0.25 && Sun.GetComponent<DayNight>().time <= 0.75)
        {
            Collider[] Enemies = Physics.OverlapSphere(transform.position, spawnRadius);
            int enemyCount = 0;
            //Checks to see if there are at least five enemies nearby
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].gameObject.tag == "Enemy")
                {
                    enemyCount += 1;
                    if (enemyCount >= 5)
                    {
                        break;
                    }
                }
            }
            //If there are less than five enemies nearby then try to find a place to spawn and then spawn a MOB
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
    //--- Check to see if there is any block directly above the current one to see if a MOB can spawn on it ---
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
    //--- Check whether there are any torches nearby as mobs cannot spawn if there are nearby torches ---
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
