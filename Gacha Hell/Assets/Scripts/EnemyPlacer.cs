using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlacer : MonoBehaviour
{
    public EnemyBase[] PossibleEnemies;
    private IEnumerator coroutine;
    protected float spawncooldownTime = 1f;
    private bool EnemyPlacerIsPlacing = true;
    public List<EnemyBase> allEnemies = null;
    public float speedIncrease = 0.95f;
    // Start is called before the first frame update
    void Start()
    {
        StartShooting();
    }

    void Update()
    {
        RemoveDestroyedEnemies();
    }

    protected void StartShooting()
    {
        coroutine = WaitAndPrint(spawncooldownTime);
        StartCoroutine(coroutine);

        print("Coroutine started");
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (EnemyPlacerIsPlacing)
        {
            waitTime *= speedIncrease;
            print("coroutine loop?");
            Spawn();
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void Spawn()
    {
        allEnemies.Add(Instantiate(PossibleEnemies[0], transform.position, Quaternion.identity, transform));
    }
    
    public void RemoveDestroyedEnemies()
    {
        for (int i = allEnemies.Count - 1; i >= 0; i--)
        {
            if (allEnemies[i] == null || allEnemies[i].gameObject == null)  // Checking if the enemy object or reference is destroyed
            {
                allEnemies.RemoveAt(i); // Remove the destroyed enemy from the list
            }
        }
    }
}
