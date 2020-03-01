using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        print("Created bullet!");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        print("Destroyed bullet!");
        Destroy(gameObject);
    }
}
