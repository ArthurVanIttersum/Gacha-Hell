using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile2 : ProjectileBase
{
    private Vector3 direction;
    protected override float speed { get { return 1.5f; } }
    public override float damage { get { return 0; } }
    protected override int pierce { get { return 3; } }
    
    public float zergDamage = 2;
    public float domgDamage = 6;
    public float tankDamage = 15;

    protected override void CalculatePath()
    {
        direction = targetPosition - startPosition;
        direction.Normalize();
        direction *= speed;
    }

    protected override void FollowPath()
    {
        transform.position += direction;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (pierced < pierce)
            {
                pierced++;
                print("projectile2 is getting triggered");
                EnemyBase enemy = other.GetComponent<EnemyBase>();
                float currentDamage = 0;
                if (enemy is Enemy1)
                {
                    currentDamage = zergDamage;
                }
                else if (enemy is Enemy2)
                {
                    currentDamage = domgDamage;
                }
                else if (enemy is Enemy3)
                {
                    currentDamage = tankDamage;
                }
                enemy.TakeDamage(currentDamage);
            }
            if (pierced == pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
