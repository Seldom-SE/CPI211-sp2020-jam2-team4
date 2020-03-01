using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnee;
    public float spawnTime;

    private int spawns;
    private bool level3;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            level3 = true;
        }

        spawns = 0;
        Invoke("SpawnEnemy", Mathf.Max(spawnTime - Time.timeSinceLevelLoad * 0.1f, spawnTime * 0.3f));
    }

    public void SpawnEnemy()
    {
        Instantiate(spawnee, transform.position, transform.rotation);
        spawns++;

        if (spawns < 6)
        {
            Invoke("SpawnEnemy", Mathf.Max(spawnTime - Time.timeSinceLevelLoad * 0.1f, spawnTime * 0.3f));
        }
    }
}
