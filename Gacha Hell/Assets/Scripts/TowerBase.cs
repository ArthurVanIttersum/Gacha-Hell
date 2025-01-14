using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TowerBase : MonoBehaviour
{
    public ProjectileBase projectile;
    private IEnumerator coroutine;
    protected virtual float shotCooldownTime { get { return 10000; } }
    private bool towerIsShooting = true;
    // Start is called before the first frame update
    void Start()
    {
        StartShooting();
    }

    protected void StartShooting()
    {
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

    protected virtual void Shoot()
    {
        
    }
}
