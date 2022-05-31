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
        
    }

    private void Update()
    {
        
    }

    public void FsmUpdate()
    {
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                break;
            case Global.EnemyFsm.Chase:
                break;
            case Global.EnemyFsm.Attack:
                break;
            case Global.EnemyFsm.AttackAfter:
                break;
        }
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Attack()
    {
        base.Attack();  // isAttacking = true;
        myAnim.SetTrigger("Attack");    // 상백 ??
        myAnim.SetBool("isAttacking", isAttacking);
    }

    public override void AttackAfter()
    {
        base.AttackAfter(); // isAttacking = false;
        myAnim.SetBool("isAttacking", isAttacking);
    }
}
