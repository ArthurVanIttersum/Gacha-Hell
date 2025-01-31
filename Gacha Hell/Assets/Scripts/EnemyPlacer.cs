using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Waves;

public class EnemyPlacer : MonoBehaviour
{
    
    private IEnumerator coroutine;
    private List <IEnumerator> coroutines;
    protected float spawncooldownTime = 1f;
    public List<EnemyBase> allEnemies = null;
    public Waves waves;
    public int _currentWave = 0;
    public event Action<int> OnWaveChanged;
    public bool autoStartActive;
    private int enemiesThisRound = 0;
    private int enemiesSpawned = 0;
    private int enemiesKilled = 0;

    public enum roundState
    {
        Spawning,
        Killing,
        Complete
    }
    public roundState currentRoundState = roundState.Complete;

    public int currentWave
    {
        get => _currentWave;
        set
        {
            if (Mathf.Abs(_currentWave - value) > Mathf.Epsilon) // Check if value changed
            {
                _currentWave = value;
                OnWaveChanged?.Invoke(_currentWave); // Trigger event
            }
        }
    }

    void Update()
    {
        RemoveDestroyedEnemies();

        switch (currentRoundState)
        {
            case roundState.Spawning:
                if (enemiesThisRound == enemiesSpawned)
                {
                    currentRoundState = roundState.Killing;
                }
            break;

            case roundState.Killing:
                if (enemiesThisRound == enemiesKilled)
                {
                    currentRoundState = roundState.Complete;
                }
                break;

            case roundState.Complete:
                if (autoStartActive)
                {
                    StartRound();
                }
            break;
        }
    }

    private void StartRound()
    {
        if (currentWave >= waves.theWaves.Length)
        {
            SceneManager.LoadScene("WinScene");
        }
        for (int i = 0; i < waves.theWaves[currentWave].clumps.Length; i++)
        {
            coroutine = Spawner(waves.theWaves[currentWave].clumps[i]);
            enemiesThisRound += waves.theWaves[currentWave].clumps[i].count;
            StartCoroutine(coroutine);
            //coroutines.Add(coroutine);
            print("Coroutine started");
        }

        currentRoundState = roundState.Spawning;
        currentWave++;
    }

    private IEnumerator Spawner(EnemyClump enemyClump)
    {
        yield return new WaitForSeconds(enemyClump.startDelay);
        for (int i = 0; i < enemyClump.count; i++)
        {
            allEnemies.Add(Instantiate(enemyClump.enemyType, transform.position, Quaternion.identity, transform));
            enemiesSpawned++;
            yield return new WaitForSeconds(enemyClump.spawncooldown);
        }
    }

    public void RemoveDestroyedEnemies()
    {
        for (int i = allEnemies.Count - 1; i >= 0; i--)
        {
            if (allEnemies[i] == null || allEnemies[i].gameObject == null)  // Checking if the enemy object or reference is destroyed
            {
                allEnemies.RemoveAt(i); // Remove the destroyed enemy from the list
                enemiesKilled++;
            }
        }
    }

    public void StartNextRound()
    {
        if (currentRoundState == roundState.Complete)
        {
            StartRound();
        }
    }
}
