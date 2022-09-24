using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
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
    private float turnTime;
    [SerializeField] private float defaulTurnTime;
    //UI
    [SerializeField] private TMP_Text countdownText;

    private void Start()
    {
        turnTime = defaulTurnTime;
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
                turnTime = defaulTurnTime;
                TurnManager.GetInstance().TriggerChangeTurn();
            }
            
        }
        
        //Update timer on UI
        countdownText.text = "Turn timer: " + turnTime.ToString().Substring(0,2);

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
    
    
}