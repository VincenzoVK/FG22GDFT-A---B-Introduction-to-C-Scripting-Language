using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turntimer : MonoBehaviour
{
    [SerializeField] private CharacterWeapon[] playersCW;
    
    //UI
    [SerializeField] private TMP_Text countdownText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update timer on UI
        if (playersCW[0].IsPlayerTurn() && playersCW[0].turnTime >= 0)
        {
            countdownText.text = "Turn timer: " + playersCW[0].turnTime.ToString().Substring(0,2);
        }
        else if (playersCW[1].IsPlayerTurn() && playersCW[1].turnTime >= 0)
        {
            countdownText.text = "Turn timer: " + playersCW[1].turnTime.ToString().Substring(0,2);
        }
    }
}
