using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile2 : ProjectileBase
{
    private Vector3 direction;
    protected override float movementSpeed { get { return 1.5f; } }
    public override float damage { get { return 0; } }
    protected override int pierce { get { return 3; } }
    [Tooltip("0 = 0%, 0.5 = 50%, 1 = 100%")]
    public float percentageOfHealth;
    protected override void CalculatePath()
    {
        direction = targetPosition - startPosition;
        direction.Normalize();
        direction *= movementSpeed;
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
                enemy.TakeDamage(enemy.maxHealth * percentageOfHealth);
            }
            if (pierced == pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
