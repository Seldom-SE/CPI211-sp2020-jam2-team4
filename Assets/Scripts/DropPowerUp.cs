using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPowerUp : MonoBehaviour
{
    public GameObject powerUp;
    private int deadEnemy;

    // Start is called before the first frame update
    void Start()
    {
        deadEnemy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (deadEnemy == 3)
        {
            Instantiate(powerUp, transform.position, transform.rotation);
        }
    }
}
