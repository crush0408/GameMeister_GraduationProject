using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        _enemyBrain.Stop();
        _enemyBrain.ani.SetBool("isWalk", false);
    }
}
