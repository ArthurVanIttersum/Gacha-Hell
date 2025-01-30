using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBase
{
    public override float speed { get { return 8f; } }
    public override float maxHealth { get { return 5f; } }
    protected override int damage { get { return 2; } }
    protected override int money { get { return 5; } }
}
