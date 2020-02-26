using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnee;
    public bool spawning = true;
    public float spawnTime;
    public float spawnDelay;
    public int enemyInRoom;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnDelay);
    }

    public void SpawnEnemy()
    {
        Instantiate(spawnee, transform.position, transform.rotation);
        if (!spawning)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
}
