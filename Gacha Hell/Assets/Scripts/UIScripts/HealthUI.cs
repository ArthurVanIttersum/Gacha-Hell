using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText; // Reference to the UI text
    private PlayerVariables playerVariables; // Reference to the PlayerVariables script

    void Start()
    {
        playerVariables = GameObject.Find("Castle").GetComponent<PlayerVariables>();
        if (playerVariables == null)
        {
            Debug.LogError("No variables found on the 'Castle' GameObject! Please make a 'Castle' GameObject with the PlayerVariables script attached to it.");
        }
    }

    void Update()
    {
        // Update the text
        if (playerVariables != null && healthText != null)
        {
            healthText.text = "Health: " + playerVariables.playerHealth.ToString("F0"); // Show as an integer
        }
    }
}
