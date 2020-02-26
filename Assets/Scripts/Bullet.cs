using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, speed * Time.deltaTime, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
