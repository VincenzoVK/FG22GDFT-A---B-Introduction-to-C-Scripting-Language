using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWeapon : MonoBehaviour
{
    [SerializeField] private PlayerTurn playerTurn;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingStartPosition;
    private bool alreadyShoot;
    public bool hasDoubleDamage;


    //Timer for turn
    public float turnTime;
    [SerializeField] private float defaultTurnTime;

    private void Start()
    {
        turnTime = defaultTurnTime;
    }

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
                turnTime = defaultTurnTime;
                TurnManager.GetInstance().TriggerChangeTurn();
            }
            
        }
    }
    
    public void ShootBullet()
    {
        GameObject newProjectile = Instantiate(projectilePrefab);
        if (hasDoubleDamage)
        {
            newProjectile.GetComponent<Projectile>()._damage *= 2;
            hasDoubleDamage = false;
        }
        newProjectile.transform.position = shootingStartPosition.position;
        newProjectile.GetComponent<Projectile>().Initialize(this);
    }

    public bool IsPlayerTurn()
    {
        return playerTurn.isPlayerTurn();
    }
    
}