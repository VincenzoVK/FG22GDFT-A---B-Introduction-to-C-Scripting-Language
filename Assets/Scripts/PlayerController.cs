using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    //Player variables
    public int health;
    
    
    //Movement variables
    private float _verticalInput;
    private float _horizontalInput;
    public float speed;
    
    //Ground check and jumping variables
    public Transform groundCheck;
    private float _groundDistance;
    private LayerMask _groundMask;
    private bool _isGrounded;
    private Rigidbody _playerRb;
    public float jumpforce;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * (_verticalInput * speed * Time.deltaTime));
        transform.Translate(Vector3.right * (_horizontalInput * speed * Time.deltaTime));

        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);
        
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _playerRb.AddForce(0, jumpforce, 0, ForceMode.Impulse);
        }

    }
}
