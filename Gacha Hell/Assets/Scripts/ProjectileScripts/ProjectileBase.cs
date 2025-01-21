using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public EnemyBase target;
    protected Vector3 startPosition;
    protected Vector3 targetPosition;
    protected virtual float lifeTime { get { return 10f; } }
    protected virtual float speed { get { return 0.001f; } }
    public virtual float damage { get { return 10000; } }
    protected virtual int pierce { get { return 1; } }
    protected int pierced = 0;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = target.transform.position;
        CalculatePath();
        StartCoroutine(DestroyAfterTime(lifeTime));
    }

    void Update()
    {
        FollowPath();
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    protected virtual void CalculatePath()
    {
        
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
