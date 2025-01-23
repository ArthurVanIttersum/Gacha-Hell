using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Tower3 : TowerBase
{
    protected override float shotCooldownTime { get { return 2.5f; } }
    protected override float range { get { return 7; } }
    public override int cost { get { return 150; } }
    protected override void Shoot()
    {
        EnemyBase target = ChooseTarget();
        if (target != null)
        {
            ProjectileBase newProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
            newProjectile.target = target;
        }
        
    }
}
