using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGroundBoss : BossBase
{
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        myFsm = Global.EnemyFsm.Idle;
        speed = 6f;
        delayTime = 2f; // 에너미 자동 공격 딜레이에 쓰겠음 
        sightDistance = 15f;    // 시야 범위
        attackDistance = 2.5f;  // 공격 범위
        rightDirection = Vector3.one;   // 오른쪽 보고 시작
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
    }

    private void Update()
    {
        if (!isDie)
        {
            FsmUpdate();
        }
    }

    public void FsmUpdate() // 매 프레임마다 실행
    {
        switch (myFsm)
        {
            case Global.EnemyFsm.None:
                break;
            case Global.EnemyFsm.Idle:
                break;
            case Global.EnemyFsm.Chase:
                break;
            case Global.EnemyFsm.Attack:
                break;
            case Global.EnemyFsm.AttackAfter:
                break;
            case Global.EnemyFsm.Meditate:
                break;
            case Global.EnemyFsm.GetHit:
                break;
            case Global.EnemyFsm.GetHitAfter:
                break;
            default:
                break;
        }
    }

    // 타겟(플레이어) 위치로 이동
    public override void Move()
    {
        base.Move();

        Vector2 dir = myTarget.transform.position - this.transform.position;
        dir.Normalize();

        myRigid.velocity = dir * speed;
    }
}
