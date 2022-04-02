using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    Transform currentPosition;
    public bool multipleHPAttack = false;
    public bool multipleShootAttack = false;
    private EnemyHealth hp;
    private void Start()
    {
        currentPosition = _enemyBrain.target;
        hp = GetComponentInParent<EnemyHealth>();
    }
    public override void TakeAction()
    {
        //공격 애니메이션 및 데미지 입히는 함수
        _enemyBrain.ani.SetBool("isWalk", false);
        _enemyBrain.target = currentPosition;

        if(multipleHPAttack)
        {
            if(hp.health >= 80)
            {
                _enemyBrain.ani.SetTrigger("isAttack");
            }
            else if(hp.health < 80 && hp.health >=50)
            {
                _enemyBrain.ani.SetTrigger("isAttackTwo");
            }
            else
            {
                _enemyBrain.ani.SetTrigger("isAttackThree");
            }
        }
        else
        {
            _enemyBrain.ani.SetTrigger("isAttack");
        }

        if(multipleShootAttack)
        {

        }
    }
}
