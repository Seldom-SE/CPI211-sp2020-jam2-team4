using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnee;
    public float spawnTime;
    public float spawnDelay;
    public AudioClip spawnZombie;
    public AudioSource audioSource;

    private int enemyInRoom;

    // Start is called before the first frame update
    void Start()
    {
        enemyInRoom = 0;
        InvokeRepeating("SpawnEnemy", spawnTime, spawnDelay);
    }

    public void SpawnEnemy()
    {
        Instantiate(spawnee, transform.position, transform.rotation);
        enemyInRoom++;
        audioSource.PlayOneShot(spawnZombie, 0.5f);

        if (enemyInRoom == 36)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
}
