using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPriestess : BossBase
{
    public override void Init()
    {
        base.Init();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (!isDie)
        {
            FsmUpdate();
        }
    }

    public void FsmUpdate()
    {
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                break;
            case Global.EnemyFsm.Chase:
                if (DistanceDecision(attackDistance))   // 공격 범위 내로 진입하면 Attack으로 상태 변경
                {
                    ChangeState(Global.EnemyFsm.Attack);
                }
                Chase();
                break;
            case Global.EnemyFsm.Attack:
                break;
            case Global.EnemyFsm.AttackAfter:
                break;
        }
    }

    public override void Attack()
    {
        base.Attack();  // isAttacking = true;
        myAnim.SetBool("isAttacking", isAttacking);
    }

    public override void AttackAfter()
    {
        base.AttackAfter(); // isAttacking = false;
        myAnim.SetBool("isAttacking", isAttacking);
    }
}
