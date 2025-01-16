using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBase
{
    protected override float speed { get { return 1f; } }
    protected override float health { get { return 10f; } }
    
    void Start()
    {
        
    }
}
