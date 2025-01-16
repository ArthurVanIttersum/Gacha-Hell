using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBase
{
    protected override float speed { get { return 5f; } }
    protected override float maxHealth { get { return 10f; } }
    protected override int damage { get { return 1; } }

}
