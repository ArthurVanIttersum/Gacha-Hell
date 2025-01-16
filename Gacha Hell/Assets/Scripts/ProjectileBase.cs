using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public EnemyBase target;
    protected Vector3 startPosition;
    protected Vector3 targetPosition;
    protected virtual float movementSpeed { get { return 0.001f; } }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        targetPosition = target.transform.position;
        CalculatePath();
    }

    protected virtual void CalculatePath()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    protected virtual void FollowPath()
    {

    }
}
