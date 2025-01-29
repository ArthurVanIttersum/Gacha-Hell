using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyBase
{
    public override float speed { get { return 6f; } }
    public override float maxHealth { get { return 30f; } }
    protected override int damage { get { return 10; } }
    protected override int money { get { return 30; } }
}
