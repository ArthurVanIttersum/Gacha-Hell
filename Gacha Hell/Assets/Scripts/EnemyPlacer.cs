using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Waves;

public class EnemyPlacer : MonoBehaviour
{
    
    private IEnumerator coroutine;
    private List <IEnumerator> coroutines;
    protected float spawncooldownTime = 1f;
    public List<EnemyBase> allEnemies = null;
    public Waves waves;
    public int currentWave = -1;
    public bool autoStartActive;
    private int enemiesThisRound = 0;
    private int enemiesSpawned = 0;
    private int enemiesKilled = 0;

    private enum roundState
    {
        Spawning,
        Killing,
        Complete
    }
    private roundState currentRoundState = roundState.Complete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        RemoveDestroyedEnemies();
        if (currentRoundState == roundState.Spawning)
        {
            if (enemiesThisRound == enemiesSpawned)
            {
                currentRoundState = roundState.Killing;
            }
        }
        if (currentRoundState == roundState.Killing)
        {
            if (enemiesThisRound == enemiesKilled)
            {
                currentRoundState = roundState.Complete;
            }
        }
        if (autoStartActive)
        {
            if (currentRoundState == roundState.Complete)
            {
                StartRound();
            }
        }
    }

    private void StartRound()
    {
        currentWave++;
        if (currentWave == waves.theWaves.Length)
        {
            //victory
            //Dylan, in this if statement you can add code to move to the win scene
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
