using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterAttackDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return !_enemyBrain.isAttacking;
    }

}
