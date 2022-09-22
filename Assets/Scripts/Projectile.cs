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
    private PlayerController temporaryPlayerController;
    public int _damage = 20;
    
    
    
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

        temporaryPlayerController = collision.gameObject.GetComponent<PlayerController>();
        isAPlayer = temporaryPlayerController != null;
        Destroy(gameObject);
        
        if (isAPlayer)
        {
            if (temporaryPlayerController.health-_damage > 0)
            {
                temporaryPlayerController.health -= _damage;
            }
            else
            { 
                collision.gameObject.SetActive(false);
                Application.Quit(); 
            }
            
        }
        else if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Wall"))
        {
            Destroy(collision.gameObject);
        }



    }

}