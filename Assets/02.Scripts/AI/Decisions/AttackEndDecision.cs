using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEndDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return !_enemyBrain.AttackEnd();
    }

    
}
