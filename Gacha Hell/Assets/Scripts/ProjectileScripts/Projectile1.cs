using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile1 : ProjectileBase
{
    private Vector3 direction;
    protected override float speed { get { return 2.5f; } }
    public override float damage { get { return 2; } }

    public float zergDamage = 2;
    public float domgDamage = 4;
    public float tankDamage = 2;
    protected override void CalculatePath()
    {
        direction = targetPosition - startPosition;
        direction.Normalize();
        direction *= speed;
        transform.rotation = Quaternion.LookRotation(direction);
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
                print("projectile1 is getting triggered");
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
