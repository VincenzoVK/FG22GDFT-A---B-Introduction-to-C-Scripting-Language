using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingStartPosition;

    public void ShootBullet()
    {
        GameObject newProjectile = Instantiate(projectilePrefab);
        newProjectile.transform.position = shootingStartPosition.position;
        newProjectile.GetComponent<Projectile>().Initialize(this);
        
    }
}