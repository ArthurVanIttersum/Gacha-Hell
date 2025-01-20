using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


public class TowerBase : MonoBehaviour
{
    public ProjectileBase projectile;
    private IEnumerator coroutine;
    protected virtual float shotCooldownTime { get { return 10000; } }
    protected virtual float range { get { return 100; } }
    public virtual int cost { get { return 0; } }
    private bool towerIsShooting = true;
    private Transform towerRange;
    private Collider towerRangeCollider;
    public List<EnemyBase> enemiesInRange = null;
    // Start is called before the first frame update
    void Start()
    {
        towerRange = transform.Find("TowerRange");
        towerRangeCollider = towerRange.GetComponent<Collider>();
        StartShooting();
        SetRange();
    }

    protected void StartShooting()
    {
        coroutine = Shooting(shotCooldownTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator Shooting(float waitTime)
    {
        while (towerIsShooting)
        {
            Shoot();
            yield return new WaitForSeconds(waitTime);
        }
    }

    protected void SetRange()
    {
        towerRange.localScale = new Vector3(range, range, range);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(other.gameObject.GetComponent<EnemyBase>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Remove(other.gameObject.GetComponent<EnemyBase>());
        }
    }

    protected EnemyBase ChooseTarget()
    {
        enemiesInRange.RemoveAll(item => item == null); // removes all null entries from the list

        //first enemy in list is the one it chooses as a target
        if (enemiesInRange == null || enemiesInRange.Count == 0)
        {  
            return null;
        }
        return enemiesInRange[0];
    }


    protected virtual void Shoot()
    {
        
    }
}
