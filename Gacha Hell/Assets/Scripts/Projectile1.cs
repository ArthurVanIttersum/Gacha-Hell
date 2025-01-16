using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile1 : ProjectileBase
{
    private Vector3 direction;
    protected override float movementSpeed { get { return 0.1f; } }
    protected override void CalculatePath()
    {
        direction = startPosition - targetPosition;
        direction.Normalize();
        direction *= movementSpeed;
    }

    protected override void FollowPath()
    {
        transform.position += direction;
    }
}
