using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile2 : ProjectileBase
{
    private Vector3 direction;
    protected override float movementSpeed { get { return 1.5f; } }
    public override float damage { get { return 3; } }
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
}
