using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(90,0,0));
        this.GetComponent<Rigidbody>().AddForce(transform.forward * speed + transform.up * speed, ForceMode.Impulse);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(GetComponent<Rigidbody>());
        Destroy(this);
    }
}
