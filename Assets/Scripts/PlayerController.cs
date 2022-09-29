using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    
    //Turn-feature variables
    [SerializeField] private PlayerTurn playerTurn;
    
    //Player statistics
    public int health;

    
    //Safety variables
    NavMeshHit hit;

    //Movement variables
    private float _verticalInput;
    private float _horizontalInput;
    public float speed;
    private float normalSpeed;

    //Rotate character and camera variables
    [SerializeField] float rotationSpeed;

    //Ground check and jumping variables
    private float _groundDistance;
    private bool _isGrounded;
    private Rigidbody _playerRb;
    public float jumpforce;
    public bool _isSafe;
    private LayerMask whatIsGround;

    //Doublejump variables
    private bool _hasMadeOneJump;
    
    //Pickup variables
    public bool hasHigherJumpPickup;
    private float normalJumpforce;
    private float speedTimer = 5;
    private float resetSpeedTimer = 5;
    private bool hasMoreSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _playerRb = this.GetComponent<Rigidbody>();
        health = 100;
        _isSafe = !NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas);
        whatIsGround = LayerMask.GetMask("whatIsGround");
        normalJumpforce = jumpforce;
        normalSpeed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        _isSafe = !NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas);
        
        if (speedTimer > 0 && hasMoreSpeed)
        {
            speedTimer -= Time.deltaTime;
        }
        else if(speedTimer <= 0)
        {
            speedTimer = resetSpeedTimer;
            ResetVelocity();
        }
        
        if (playerTurn.isPlayerTurn())
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, whatIsGround);
            
            if (_horizontalInput != 0)
            {
                transform.Translate(Vector3.right * (_horizontalInput * speed * Time.deltaTime));
            }
            
            if (_verticalInput != 0)
            {
                transform.Translate(Vector3.forward * (_verticalInput * speed * Time.deltaTime));
            }
            
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            }
            
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _hasMadeOneJump = true;
                if (hasHigherJumpPickup)
                {
                    jumpforce *= 2;
                }
                _playerRb.AddForce(0f, jumpforce, 0f, ForceMode.Impulse);
                hasHigherJumpPickup = false;
                jumpforce = normalJumpforce;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !_isGrounded && _hasMadeOneJump)
            {
                _playerRb.AddForce(transform.forward.x, (float)(jumpforce/(1.5)), transform.forward.z, ForceMode.Impulse);
                _hasMadeOneJump = false;
            }
        }
    }

    public void ModifyVelocity(float addedVelocity)
    {
        speed += addedVelocity;
        hasMoreSpeed = true;
    }

    public void ResetVelocity()
    {
        speed = normalSpeed;
        hasMoreSpeed = false;
    }
}
