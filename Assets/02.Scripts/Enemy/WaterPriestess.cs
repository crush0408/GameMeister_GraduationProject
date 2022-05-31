using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPriestess : BossBase
{
    public int hitCount;

    public override void Init()
    {
        base.Init();
        myFsm = Global.EnemyFsm.Idle;
        speed = 6f;

        sightDistance = 10f;    // 시야 범위
        attackDistance = 6f;    // 공격 범위
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
                if (!isAttacking)
                {
                    ChangeState(Global.EnemyFsm.Idle);
                }
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

    public override void DeadAnimScript()   // 이렇게 할 거면 override가 필요한가??
    {
        base.DeadAnimScript();  // Destroy(this.gameObject)
    }
}
