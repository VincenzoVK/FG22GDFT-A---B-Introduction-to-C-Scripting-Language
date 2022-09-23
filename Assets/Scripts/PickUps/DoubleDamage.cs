using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamage : MonoBehaviour
{

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
            collision.gameObject.GetComponent<CharacterWeapon>().hasDoubleDamage = true;
            Destroy(this);
            Destroy(gameObject);    
        }
    }
}