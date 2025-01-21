using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile3 : ProjectileBase
{
    private Vector3 direction;
    protected override float movementSpeed { get { return 0.5f; } }
    public override float damage { get { return 4; } }
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
