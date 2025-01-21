using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveText; // Reference to the UI text
    private EnemyPlacer enemyPlacer; // Reference to the PlayerVariables script

    void Start()
    {
        enemyPlacer = GameObject.Find("EnemyPlacer").GetComponent<EnemyPlacer>();
        if (enemyPlacer == null)
        {
            Debug.LogError("No variables found on the 'EnemyPlacer' GameObject! Please make a 'EnemyPlacer' GameObject with the enemyPlacer script attached to it.");
        }
    }

    void Update()
    {
        // Update the text
        if (enemyPlacer != null && waveText != null)
        {
            waveText.text = "Wave: " + enemyPlacer.currentWave.ToString("F0"); // Show as an integer
        }
    }
}
