using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        //���� �ִϸ��̼� �� ������ ������ �Լ�
        _enemyBrain.ani.SetBool("isWalk", false);
        _enemyBrain.ani.SetTrigger("isAttack");
    }
}
