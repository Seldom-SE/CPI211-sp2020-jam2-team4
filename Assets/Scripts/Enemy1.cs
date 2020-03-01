using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    private int numShot;

    public GameObject powerUp;
    public AudioClip deadZombie;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        numShot = 0;
    }

    /* Shot or die */
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            if (numShot < 2)
            {
                numShot++;
            }
            else
            {
                PlayerController playerController = player.GetComponent<PlayerController>();
                int killedEnemy = playerController.IncrementZombiesKilled();
                if (killedEnemy % 3 == 0)
                {
                    Instantiate(powerUp, transform.position, transform.rotation);
                }
                audioSource.PlayOneShot(deadZombie, 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
