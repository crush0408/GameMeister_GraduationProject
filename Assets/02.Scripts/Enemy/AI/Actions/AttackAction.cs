using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        
        if (!_enemyBrain.isAttacking)
        {
            _enemyBrain.isAttacking = true;
            _enemyBrain.FlipSprite();
            _enemyBrain.Stop();
            _enemyBrain.Attack();
        }
        
        Debug.Log("Attack");
    }

    
}
