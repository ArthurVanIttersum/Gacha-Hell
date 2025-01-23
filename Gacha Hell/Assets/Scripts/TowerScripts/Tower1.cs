using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Tower1 : TowerBase
{
    protected override float shotCooldownTime { get { return 0.5f; } }
    protected override float range { get { return 11; } }
    public override int cost { get { return 50; } }
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
