using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    Transform currentPosition;
    private void Start()
    {
        currentPosition = _enemyBrain.target;
    }
    public override void TakeAction()
    {
        //공격 애니메이션 및 데미지 입히는 함수
        _enemyBrain.ani.SetBool("isWalk", false);
        _enemyBrain.ani.SetTrigger("isAttack");
        _enemyBrain.target = currentPosition;
    }
}
