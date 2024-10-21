using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private Image healthBar;
    private float currentHealth, maxHealth;
    public TMP_Text healthText; 
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = player.maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = player.health;
        if(currentHealth <=0){
            currentHealth = 0;
        }
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        float targetFillAmount = currentHealth/maxHealth;
        healthBar.fillAmount = targetFillAmount;
        maxHealth = player.maxHealth;
    }
}
