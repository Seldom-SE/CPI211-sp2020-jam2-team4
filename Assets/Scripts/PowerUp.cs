using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip collected;
    public AudioSource audioSource;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(collected, 0.5f);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.up, Time.fixedDeltaTime * 90f);
    }
}
