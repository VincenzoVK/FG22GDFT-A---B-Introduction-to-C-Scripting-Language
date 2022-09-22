using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineCameraController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera[] cameras;
    private int numberOfCameras;
    private int counterofCamera;
    
    
    // Start is called before the first frame update
    void Start()
    {
        numberOfCameras = cameras.Length;
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        counterofCamera = 0;
        cameras[counterofCamera].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeCamera()
    {
        cameras[counterofCamera].gameObject.SetActive(false);
        counterofCamera++;
        
        //Check if you arrived at the end of the array
        if (counterofCamera == cameras.Length)
        {
            counterofCamera = 0;
        }

        cameras[counterofCamera].gameObject.SetActive(true);

    }
}
