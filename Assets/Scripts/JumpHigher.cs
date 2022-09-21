using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHigher : MonoBehaviour
{

    [SerializeField] public float jumpForce = 20;
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
            collision.gameObject.GetComponent<PlayerController>().jumpforce += jumpForce;
            Destroy(this);
            Destroy(gameObject);    
        }
    }
}