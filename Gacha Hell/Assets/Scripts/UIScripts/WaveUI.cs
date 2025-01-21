using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveText; // Reference to the UI text
    private EnemyPlacer enemyPlacer; // Reference to the PlayerVariables script

    void Awake() // Not in start since we want to subscribe to the event before it is called
    {
        enemyPlacer = GameObject.Find("EnemyPlacer").GetComponent<EnemyPlacer>();
        if (enemyPlacer == null)
        {
            Debug.LogError("No variables found on the 'EnemyPlacer' GameObject! Please make a 'EnemyPlacer' GameObject with the enemyPlacer script attached to it.");
        }
        UpdateWaveText(enemyPlacer.currentWave);
    }

    void UpdateWaveText(int newWave)
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + newWave.ToString("F0");
        }
    }

    // --------- Event Subscriptions ---------
    void OnEnable() // Subscribe to the event
    {
        if (enemyPlacer != null)
        {
            enemyPlacer.OnWaveChanged += UpdateWaveText;
        }
    }

    void OnDisable() // Unsubscribe from the event if the object is disabled
    {
        if (enemyPlacer != null)
        {
            enemyPlacer.OnWaveChanged -= UpdateWaveText;
        }        
    }
}
