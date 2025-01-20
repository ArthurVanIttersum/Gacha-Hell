using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Splines;

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

    public enum TargetingOptions
    {
        First,
        Last,
        Close,
        Preferred
    }
    public TargetingOptions currentTargeting = TargetingOptions.Close;
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

        if (enemiesInRange == null || enemiesInRange.Count == 0)// make sure the list makes sense
        {  
            return null;
        }

        float value = float.MaxValue;
        int lastBest = 0;
        if (currentTargeting == TargetingOptions.Close)
        {
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                float distance = Vector3.Distance(enemiesInRange[i].transform.position, transform.position);
                if (distance < value)
                {
                    value = distance;
                    lastBest = i;
                }
            }
        }
        if (currentTargeting == TargetingOptions.Last)
        {
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                float distance = enemiesInRange[i].GetComponent<SplineAnimate>().ElapsedTime * enemiesInRange[i].speed;
                if (distance < value)
                {
                    value = distance;
                    lastBest = i;
                }
            }
        }
        if (currentTargeting == TargetingOptions.First)
        {
            value = 0;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                float distance = enemiesInRange[i].GetComponent<SplineAnimate>().ElapsedTime * enemiesInRange[i].speed;
                if (distance > value)
                {
                    value = distance;
                    lastBest = i;
                }
            }
        }
        if (currentTargeting == TargetingOptions.Preferred)
        {//not properly implemented yet
            //waiting for designer answer
            value = 0;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                float distance = enemiesInRange[i].GetComponent<SplineAnimate>().ElapsedTime * enemiesInRange[i].speed;
                if (distance > value)
                {
                    value = distance;
                    lastBest = i;
                }
            }
        }


        return enemiesInRange[lastBest];
    }


    protected virtual void Shoot()
    {
        
    }
}
