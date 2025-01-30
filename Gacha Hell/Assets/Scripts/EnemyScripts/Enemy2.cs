using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBase
{
    public override float speed { get { return 16f; } }
    public override float maxHealth { get { return 12f; } }
    protected override int damage { get { return 6; } }
    protected override int money { get { return 12; } }
}
