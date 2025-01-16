using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEngine.Rendering.DebugUI;

public class TowerBase : MonoBehaviour
{
    public ProjectileBase projectile;
    private IEnumerator coroutine;
    protected virtual float shotCooldownTime { get { return 10000; } }
    protected virtual float range { get { return 10000; } }
    public virtual int cost { get { return 0; } }
    private bool towerIsShooting = true;
    public EnemyPlacer enemyPlacer;
    public List<EnemyBase> SomeEnemies = null;
    // Start is called before the first frame update
    void Start()
    {
        StartShooting();
    }

    protected void StartShooting()
    {
        print("towerbaseStartshootingIsBeingActivated");
        coroutine = WaitAndPrint(shotCooldownTime);
        StartCoroutine(coroutine);

        print("Coroutine started");
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (towerIsShooting)
        {
            print("coroutine loop?");
            Shoot();
            yield return new WaitForSeconds(waitTime);
        }
    }

    protected EnemyBase[] GetEnemiesInrange()
    {
        if (enemyPlacer.allEnemies == null)
        {
            return null;
        }
        
        for (int i = 0; i < enemyPlacer.allEnemies.Count; i++)
        {
            Debug.Log("something is happening in the loop");
            //Debug.Log(SomeEnemies);
            print(enemyPlacer.allEnemies[i]);
            Vector3 position = enemyPlacer.allEnemies[i].transform.position;
            if (Vector3.Distance(position, transform.position) < range)
            {
                SomeEnemies.Add(enemyPlacer.allEnemies[i]);

            }
            else
            {
                Debug.Log("could not find enemy");
            }
        }
        if (SomeEnemies == null)
        {
            return null;
        }

        return SomeEnemies.ToArray();

    }

    protected EnemyBase ChooseTarget()
    {
        //keeping it simple for now.
        //first enemy in list is the one it chooses as a target
        EnemyBase[] enemiesInrange = GetEnemiesInrange();
        if (enemiesInrange == null)
        {
            return null;
        }
        return enemiesInrange[0];
    }


    protected virtual void Shoot()
    {
        
    }
}
