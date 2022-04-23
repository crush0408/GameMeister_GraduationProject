using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitJudgeDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return !_enemyBrain.getHit;
    }
}
