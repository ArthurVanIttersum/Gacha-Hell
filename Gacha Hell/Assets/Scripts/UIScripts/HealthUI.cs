using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText; // Reference to the UI text
    private PlayerVariables playerVariables; // Reference to the PlayerVariables script

    void Awake() // Not in start since we want to subscribe to the event before it is called
    {
        playerVariables = GameObject.Find("Castle").GetComponent<PlayerVariables>();
        if (playerVariables == null)
        {
            Debug.LogError("No variables found on the 'Castle' GameObject! Please make a 'Castle' GameObject with the PlayerVariables script attached to it.");
        }
        UpdateHealthText(playerVariables.playerHealth);
    }

    void UpdateHealthText(int newHealth)
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + newHealth.ToString("F0");
        }
    }

    // --------- Event Subscriptions ---------
    void OnEnable() // Subscribe to the event
    {
        if (playerVariables != null)
        {
            playerVariables.OnHealthChanged += UpdateHealthText;
        }
    }

    void OnDisable() // Unsubscribe from the event if the object is disabled
    {
        if (playerVariables != null)
        {
            playerVariables.OnHealthChanged -= UpdateHealthText;
        }        
    }
}
