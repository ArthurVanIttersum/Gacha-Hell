using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile1 : ProjectileBase
{
    private Vector3 direction;
    protected override float speed { get { return 0.5f; } }
    public override float damage { get { return 5; } }
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
}
