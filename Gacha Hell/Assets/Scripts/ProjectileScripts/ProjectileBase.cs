using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public EnemyBase target;
    protected Vector3 startPosition;
    protected Vector3 targetPosition;
    protected float lifeTime = 0;
    protected virtual float movementSpeed { get { return 0.001f; } }
    public virtual float damage { get { return 10000; } }
    protected virtual int pierce { get { return 1; } }
    protected int pierced = 0;
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
        lifeTime += Time.deltaTime;
        if (lifeTime > 10)
        {
            Destroy(gameObject);
        }
    }
    

    protected virtual void FollowPath()
    {

    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (pierced < pierce)
            {
                pierced++;
                other.GetComponent<EnemyBase>().TakeDamage(damage);
            }
            if (pierced == pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
