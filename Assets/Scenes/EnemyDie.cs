using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    private int numShot;

    // Start is called before the first frame update
    void Start()
    {
        numShot = 0;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (numShot < 2)
                numShot++;
            else if (numShot == 2)
                Destroy(gameObject);
        }
    }
}
