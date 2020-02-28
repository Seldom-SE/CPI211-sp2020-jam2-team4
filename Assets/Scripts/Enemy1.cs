using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private int numShot;

    // Start is called before the first frame update
    void Start()
    {
        numShot = 0;
    }

    /* Shot or die */
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (numShot < 2)
            {
                numShot++;
            }
            else if (numShot == 2)
            {
                col.gameObject.GetComponent<PlayerController>().zombie1Killed++;
                Destroy(gameObject);
            }
        }

    }
}
