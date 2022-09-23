using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreVelocity : MonoBehaviour
{

    [SerializeField] public float addedVelocity;
    private bool doOnce;
    
    // Start is called before the first frame update
    void Start()
    {
        doOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && doOnce)
        {
            doOnce = false;
            collision.gameObject.GetComponent<PlayerController>().ModifyVelocity(addedVelocity);
            Destroy(this);
            Destroy(gameObject);    
        }
    }
}
