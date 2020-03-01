using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnee;
    public float spawnTime;
    public GameObject player;

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
        GameObject zombie = Instantiate(spawnee, transform.position, transform.rotation);
        zombie.GetComponent<Enemy>().SetPlayer(player);
        spawns++;

        if (level3 || spawns < 6)
        {
            Invoke("SpawnEnemy", Mathf.Max(spawnTime - Time.timeSinceLevelLoad * 0.1f, spawnTime * 0.3f));
        }
    }
}
