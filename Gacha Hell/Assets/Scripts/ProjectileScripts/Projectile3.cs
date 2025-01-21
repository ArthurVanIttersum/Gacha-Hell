using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Projectile3 : ProjectileBase
{
    private Vector3 direction;
    protected override float movementSpeed { get { return 0.5f; } }
    public override float damage { get { return 4; } }
    protected override int pierce { get { return 1; } }
    protected override void CalculatePath()
    {
        direction = targetPosition - startPosition;
        direction.Normalize();
        direction *= movementSpeed;
    }
    public LayerMask layerMask;
    public int explosivePierce;
    public int explosiveRange;
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
                Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRange, layerMask);
                //maybe add code to start a particle effect of an explosion here
                if (colliders == null || colliders.Length == 0)// make sure the list makes sense
                {
                    return;
                }
                List<Collider> listColliders = colliders.ToList<Collider>();
                int lastBest = 0;
                float value = float.MaxValue;
                float distance;
                for (int i = 0; i < explosivePierce; i++)
                {
                    for (int j = 0; j < listColliders.Count; j++)
                    {
                        distance = Vector3.Distance(listColliders[j].transform.position, transform.position);
                        if (distance < value)
                        {
                            value = distance;
                            lastBest = j;
                        }
                    }
                    listColliders[lastBest].GetComponent<EnemyBase>().TakeDamage(damage);
                    listColliders.RemoveAt(lastBest);
                    lastBest = 0;
                    value = float.MaxValue;
                    if (listColliders == null || listColliders.Count == 0)// make sure the list makes sense
                    {
                        break;
                    }
                }
            }
            if (pierced == pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
