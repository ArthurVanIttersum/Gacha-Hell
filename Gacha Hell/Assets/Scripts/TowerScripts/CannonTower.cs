using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class CannonTower : TowerBase
{
    protected override float shotCooldownTime { get { return 2f; } }
    protected override float range { get { return 7; } }
    public override int cost { get { return 400; } }
    protected override void Shoot()
    {
        EnemyBase target = ChooseTarget();
        if (target != null)
        {
            ProjectileBase newProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
            newProjectile.target = target;
            RotateToTarget(target);
        }
        
    }

    protected override void RotateToTarget(EnemyBase target)
    {
        Vector3 direction = target.transform.position - transform.position;

        transform.Find("cannon_tower").transform.rotation = Quaternion.LookRotation(direction);

    }
}
