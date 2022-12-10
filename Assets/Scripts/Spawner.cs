using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bandit;
    public GameObject boss;

    public float spawnRate;
    public int numBandits;
    private float nextSpawn = 0f;
    public float timeSpawnBoss;
    private bool spawnBoss = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn && numBandits>0){
            nextSpawn = Time.time + spawnRate;

            Instantiate(bandit, transform.position, bandit.transform.rotation);
            numBandits--;
        }

        if(numBandits<=0){
            timeSpawnBoss -= Time.deltaTime;
        }

        if(numBandits <= 0 && spawnBoss && timeSpawnBoss<=0){
            Instantiate(boss, transform.position, boss.transform.rotation);
            spawnBoss = false;
        }
    }
}
