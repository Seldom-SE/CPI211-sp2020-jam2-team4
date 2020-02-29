using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip collected;
    AudioSource audioSource;

    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this);
            audioSource.PlayOneShot(collected, 0.5f);
        }
    }

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }
}
