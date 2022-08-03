using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public bool hasBeenKilled = false;

    protected override void Death()
    {
        base.Death();
        hasBeenKilled = true;
    }
}
