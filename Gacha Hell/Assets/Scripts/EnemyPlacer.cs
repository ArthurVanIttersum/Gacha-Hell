using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlacer : MonoBehaviour
{
    public EnemyBase[] PossibleEnemies;
    private IEnumerator coroutine;
    protected float spawncooldownTime = 1f;
    private bool EnemyPlacerIsPlacing = true;
    public List<EnemyBase> allEnemies;
    // Start is called before the first frame update
    void Start()
    {
        StartShooting();
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
            print("coroutine loop?");
            Spawn();
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void Spawn()
    {
        allEnemies.Add(Instantiate(PossibleEnemies[0], transform.position, Quaternion.identity, transform));
    }
}
