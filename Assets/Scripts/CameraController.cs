using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject[] player;
    [SerializeField] private Vector3 offsetPlayer1;
    [SerializeField] private Vector3 offsetPlayer2;
    [SerializeField] private Vector3 cameraRotationPlayer1;
    [SerializeField] private Vector3 cameraRotationPlayer2;
    [SerializeField] float horizontalCameraSpeed;
    [SerializeField] float verticalCameraSpeed;
    private float horizontalMouseInput;
    private float verticalMouseInput;

    private int playerTurnIndex;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player[0] == null || player[1] == null)
        {
            Application.Quit();
        }
        else
        {
            if (player[0].GetComponent<PlayerTurn>().isPlayerTurn())
            {
                playerTurnIndex = 0;
            }
            else
            {
                playerTurnIndex = 1;
            }
        
        
            if (playerTurnIndex == 0 && player != null)
            {
                this.transform.eulerAngles = cameraRotationPlayer1;
                transform.position = player[playerTurnIndex].transform.position - offsetPlayer1;
            }
            else if(player != null)
            {
                this.transform.eulerAngles = cameraRotationPlayer2;
                transform.position = player[playerTurnIndex].transform.position - offsetPlayer2;
            }
        }
        

    }
}
