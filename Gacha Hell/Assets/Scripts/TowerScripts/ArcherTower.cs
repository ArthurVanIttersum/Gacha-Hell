using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class ArcherTower: TowerBase

{
    protected override float shotCooldownTime { get { return 0.9f; } }
    protected override float range { get { return 11; } }
    public override int cost { get { return 100; } }
    private EnemyBase target1 = null;
    private EnemyBase target2 = null;
    private EnemyBase target3 = null;
    private IEnumerator burstCoroutine;
    protected override void Shoot()
    {
        ChooseTargets();
        burstCoroutine = BurstShot();
        StartCoroutine(burstCoroutine);
    }

    private void ChooseTargets()
    {
        target1 = ChooseTarget();//find first target
        if (target1 == null)
        {
            return;
        }
        bool containsAnotherEnemy = enemiesInRange.Any(item => item != target1);//test if there are other enemies in range
        if (!containsAnotherEnemy)
        {//unload all shots on the first enemy
            target2 = target1;
            target3 = target1;
            print("1 enemy");
            return;
        }
        enemiesInRange.Remove(target1);//remove and re-add later, so the ChooseTarget function finds a different target
        target2 = ChooseTarget();//find second target
        if (target2 == null)
        {
            return;
        }
        containsAnotherEnemy = enemiesInRange.Any(item => item != target2);//test if there are other enemies in range
        if (!containsAnotherEnemy)
        {
            target3 = target1;
            enemiesInRange.Add(target1);//add the enemy back in the list
            print("2 enemy");
            return;
        }
        enemiesInRange.Remove(target2);//remove and re-add later, so the ChooseTarget function finds a different target
        target3 = ChooseTarget();//find second target
        if (target3 == null)
        {
            return;
        }
        enemiesInRange.Add(target1);//add the enemy back in the list
        enemiesInRange.Add(target2);//add the enemy back in the list
        print("3 enemy");
    }

    private IEnumerator BurstShot()
    {
        if (target1 == null)
        {
            yield break;
        }
        ProjectileBase newProjectile;
        newProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        newProjectile.target = target1;
        RotateToTarget(target1);
        yield return new WaitForSeconds(0.1f);
        if (target2 == null)
        {
            yield break;
        }
        newProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        newProjectile.target = target2;
        RotateToTarget(target2);
        yield return new WaitForSeconds(0.1f);
        if (target3 == null)
        {
            yield break;
        }
        newProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        newProjectile.target = target3;
        RotateToTarget(target3);
        yield break;
    }

    protected override void RotateToTarget(EnemyBase target)
    {
        Vector3 direction = target.transform.position - transform.position;

        transform.Find("archer_tower").transform.rotation = Quaternion.LookRotation(direction);

    }


}
