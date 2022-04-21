using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitAction : AIAction
{
    public override void TakeAction()
    {
        
        _enemyBrain.GetHit();


    }
}
