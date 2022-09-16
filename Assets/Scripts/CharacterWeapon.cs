using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    [SerializeField] private PlayerTurn playerTurn;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingStartPosition;


    private void Update()
    {
        bool IsPlayerTurn = playerTurn.isPlayerTurn();
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPlayerTurn)
            {
                TurnManager.GetInstance().TriggerChangeTurn();
                ShootBullet();
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