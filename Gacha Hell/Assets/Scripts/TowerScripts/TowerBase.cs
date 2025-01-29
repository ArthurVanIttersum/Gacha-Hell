using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Splines;
using System.Linq;
using TMPro;

public class TowerBase : MonoBehaviour
{
    public ProjectileBase projectile;
    private IEnumerator coroutine;
    protected virtual float shotCooldownTime { get { return 10000; } }
    protected virtual float range { get { return 100; } }
    public virtual int cost { get { return 0; } }
    
    
    
    private Transform towerRange;
    private Collider towerRangeCollider;
    public List<EnemyBase> enemiesInRange = null;
    public EnemyBase preferredEnemy;

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
        while (true)
        {
            if (TestTowerActive())
            {
                Shoot();
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    public bool TestTowerActive()
    {
        enemiesInRange.RemoveAll(item => item == null); // removes all null entries from the list
        bool enoughEnemiesInRange = (enemiesInRange.Count > 0);
        return enoughEnemiesInRange;
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
        switch (currentTargeting)
        {
            case TargetingOptions.Close:
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    float distance = Vector3.Distance(enemiesInRange[i].transform.position, transform.position);
                    if (distance < value)
                    {
                        value = distance;
                        lastBest = i;
                    }
                }
                break;
            case TargetingOptions.Last:
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    float distance = enemiesInRange[i].GetComponent<SplineAnimate>().ElapsedTime * enemiesInRange[i].speed;
                    if (distance < value)
                    {
                        value = distance;
                        lastBest = i;
                    }
                }
                break;
            case TargetingOptions.First:
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
                break;
            case TargetingOptions.Preferred:
                Type type = preferredEnemy.GetType();
                bool containsPreferedEnemy = enemiesInRange.Any(item => item.GetType() == type);
                if (!containsPreferedEnemy)
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
                    break;
                }
                else
                {
                    
                }
                List<EnemyBase> enemiesOfPreferredType = new List<EnemyBase>{};
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i].GetType() == preferredEnemy.GetType())
                    {
                        enemiesOfPreferredType.Add(enemiesInRange[i]);
                    }
                }
                if (enemiesOfPreferredType == null || enemiesOfPreferredType.Count == 0)// make sure the list makes sense
                {
                    print("ListIsEmpty");
                    return null;
                }
                value = 0;
                for (int i = 0; i < enemiesOfPreferredType.Count; i++)
                {
                    float distance = enemiesOfPreferredType[i].GetComponent<SplineAnimate>().ElapsedTime * enemiesOfPreferredType[i].speed;
                    if (distance > value)
                    {
                        value = distance;
                        lastBest = i;
                    }
                }
                return enemiesOfPreferredType[lastBest];
            default:
                print("warning : chosen target option is undifined");
                break;
        }
        return enemiesInRange[lastBest];
    }


    protected virtual void Shoot()
    {
        
    }

    protected void RotateToTarget(EnemyBase target)
    {
        Vector3 direction = target.transform.position - this.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
