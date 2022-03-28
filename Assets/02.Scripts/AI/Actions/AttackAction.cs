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
        //���� �ִϸ��̼� �� ������ ������ �Լ�
        _enemyBrain.ani.SetBool("isWalk", false);
        _enemyBrain.ani.SetTrigger("isAttack");
        _enemyBrain.target = currentPosition;
    }
}
