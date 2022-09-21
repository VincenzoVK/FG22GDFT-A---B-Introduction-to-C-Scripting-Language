using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject[] player;
    [SerializeField] private Vector3 offsetPlayer1;
    [SerializeField] private Vector3 offsetPlayer2;
    
    
    //Mouse camera rotation variables
    [SerializeField] private float mouseSensitivity = 3.0f;
    private float rotationY;
    private float rotationX;
    [SerializeField] private float distanceFromTarget = 5.0f;
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector2 rotationXMinMax = new Vector2(-40, 40);
    
    //Turn variables
    private int playerTurnIndex;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        rotationY += mouseX;
        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
        
        Vector3 nextRotation = new Vector3(rotationX, rotationY);
        
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;


        if (player[0] == null || player[1] == null)
        {
            Application.Quit();
        }
        else
        {
            if (player[0].GetComponent<PlayerTurn>().isPlayerTurn())
            {
                playerTurnIndex = 0;
                if (player != null)
                {
                    transform.position = player[playerTurnIndex].transform.position - transform.forward * distanceFromTarget;
                }
            }
            else if(player != null)
            {
                playerTurnIndex = 1;
                transform.position = player[playerTurnIndex].transform.position - transform.forward * distanceFromTarget;
            }
        }

        

    }
}
