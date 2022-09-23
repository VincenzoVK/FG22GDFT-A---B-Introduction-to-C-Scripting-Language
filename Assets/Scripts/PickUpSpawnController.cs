using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpSpawnController : MonoBehaviour
{
    [SerializeField] private float frequencyOfSpawn = 5;
    [SerializeField] private float timer;
    [SerializeField] private GameObject[] pickups;
    private int topRangeRandomNumber;
    private float bounds = 150;
    private float y = 9.5f;
    private Vector3 spawnPoint;
    private bool isColliding;
    private RaycastHit hit;
    private float randomZ;
    private float randomX;

    private int randomNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = frequencyOfSpawn;
        topRangeRandomNumber = pickups.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            randomNumber = Random.Range(0, topRangeRandomNumber);
            randomZ = Random.Range(-93, -45);
            randomX = Random.Range(-48, 80);

            spawnPoint = new Vector3(randomX, y, randomZ);
            
            isColliding = Physics.SphereCast(spawnPoint, 20f, Vector3.zero, out hit, 0f);
            
            while (isColliding)
            {
                randomZ = Random.Range(-bounds, bounds);
                randomX = Random.Range(-bounds, bounds);

                spawnPoint = new Vector3(randomX, y, randomZ);
            
                isColliding = Physics.SphereCast(spawnPoint, 20f, Vector3.zero, out hit, 0f);
            }
            
            GameObject newPickup = Instantiate(pickups[randomNumber]);
            newPickup.transform.position = spawnPoint; 
            timer = frequencyOfSpawn;
        }
    }
}
