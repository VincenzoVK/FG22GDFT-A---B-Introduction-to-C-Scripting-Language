using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    
    //Turn-feature variables
    [SerializeField] private PlayerTurn playerTurn;
    
    //Player statistics
    public int health;


    //Movement variables
    private float _verticalInput;
    private float _horizontalInput;
    public float speed;

    //Rotate character and camera variables
    [SerializeField] float horizontalCameraSpeed;
    [SerializeField] float verticalCameraSpeed;
    private float horizontalMouseInput;
    private float verticalMouseInput;
    

    //Ground check and jumping variables
    private float _groundDistance;
    private LayerMask _groundMask;
    private bool _isGrounded;
    private Rigidbody _playerRb;
    public float jumpforce;
    
    //Doublejump variables
    private bool _hasMadeOneJump;


    // Start is called before the first frame update
    void Start()
    {
        _playerRb = this.GetComponent<Rigidbody>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn.isPlayerTurn())
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            horizontalMouseInput = Input.GetAxis("Mouse X");
            verticalMouseInput = Input.GetAxis("Mouse Y");

            _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);

            if (_horizontalInput != 0)
            {
                transform.Translate(Vector3.right * (_horizontalInput * speed * Time.deltaTime));
            }
            
            if (_verticalInput != 0)
            {
                transform.Translate(Vector3.forward * (_verticalInput * speed * Time.deltaTime));
            }
            
            transform.Rotate(0, horizontalCameraSpeed * horizontalMouseInput * Time.deltaTime, 0);

                if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _hasMadeOneJump = true;
                _playerRb.AddForce(0f, jumpforce, 0f, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.Space) && !_isGrounded && _hasMadeOneJump)
            {
                _playerRb.AddForce(transform.forward.x, (float)(jumpforce/(1.5)), transform.forward.z, ForceMode.Impulse);
                _hasMadeOneJump = false;
            }

        }
    }


}
