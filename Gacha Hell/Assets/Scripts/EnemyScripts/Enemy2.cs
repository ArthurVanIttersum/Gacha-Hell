using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBase
{
    public override float speed { get { return 10f; } }
    protected override float maxHealth { get { return 10f; } }
    protected override int damage { get { return 1; } }
    protected override int money { get { return 5; } }
}
