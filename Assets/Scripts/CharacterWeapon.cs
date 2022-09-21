using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    [SerializeField] private PlayerTurn playerTurn;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingStartPosition;
    private bool alreadyShoot;
    
    //Timer for turn
    [SerializeField] float turnTime = 30f;


    private void Update()
    {
        bool IsPlayerTurn = playerTurn.isPlayerTurn();
        if (IsPlayerTurn)
        {
            if (turnTime > 0)
            {
                turnTime -= Time.deltaTime;
            
                if (Input.GetMouseButtonDown(0) && !alreadyShoot)
                {
                    ShootBullet();
                    alreadyShoot = true;
                }
            }
            else
            {
                alreadyShoot = false;
                turnTime = 30f;
                TurnManager.GetInstance().TriggerChangeTurn();
            }
            
        }
        
    }
    
    public void ShootBullet()
    {
        GameObject newProjectile = Instantiate(projectilePrefab);
        newProjectile.transform.position = shootingStartPosition.position;
        newProjectile.GetComponent<Projectile>().Initialize(this);
    }
    
    
}