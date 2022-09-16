using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] public float speed = 10;
    private bool isActive;
    private CharacterWeapon localCharacterWeapon;
    private bool isAPlayer;
    
    
    
    public void Initialize(CharacterWeapon characterWeapon)
    {
        isActive = true;
        localCharacterWeapon = characterWeapon;
    }

    private void Update()
    {

        if (isActive)
        {
            transform.Translate((localCharacterWeapon.transform.forward * (speed * Time.deltaTime)));
        }
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        isAPlayer = collision.gameObject.GetComponent<PlayerController>() != null;
        
        Destroy(collision.gameObject);
        Destroy(gameObject);
        
        //If the gameObject you just destroyed is a player, quit the game since it's finished
        if (isAPlayer)
        {
            Debug.LogError("Finish the game");
            Application.Quit();
        }
        
    }

}