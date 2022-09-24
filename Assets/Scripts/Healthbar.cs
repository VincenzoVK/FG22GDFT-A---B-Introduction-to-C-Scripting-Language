using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    private Image healthBar;
    private const float MAX_HEALTH = 100f;

    [SerializeField] private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isActiveAndEnabled)
        {
            healthBar.fillAmount = player.health / MAX_HEALTH;
        }
        else
        {
            healthBar.fillAmount = 0;
        }
    }
}
