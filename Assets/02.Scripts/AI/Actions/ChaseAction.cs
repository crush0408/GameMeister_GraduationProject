using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 dir = _enemyBrain.target.position - transform.position;
        _enemyBrain.Move(dir.normalized);
        _enemyBrain.ani.SetBool("isWalk", true);
    }
}
