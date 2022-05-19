using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGroundBoss : BossBase
{
    private IEnumerator delayCoroutine;

    private int attackNum = 0;  // 연속 공격 횟수(1-2회 : atk2 / 3회 : atk3) -> Idle 상태로 돌아가며 초기화

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        myFsm = Global.EnemyFsm.Idle;
        speed = 6f;
        delayTime = 1.5f;
        sightDistance = 15f;    // 시야 범위
        attackDistance = 3.5f;  // 공격 범위
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
            case Global.EnemyFsm.Idle:
                delayCoroutine = Delay(2f, Global.EnemyFsm.Chase);
                StartCoroutine(delayCoroutine);
                break;
            case Global.EnemyFsm.Chase:
                Debug.Log("Chase 모드 진입");
                Chase();
                break;
            case Global.EnemyFsm.Attack:
                Debug.Log("Attack 모드 진입");
                Attack();
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
        dir.y = 0f;
        dir.Normalize();

        myVelocity = dir * speed;
        myRigid.velocity = myVelocity;
    }

    public IEnumerator Delay(float delay, Global.EnemyFsm enemyFsm)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(enemyFsm);
    }

    private void Chase()
    {
        if (DistanceDecision(attackDistance))   // 공격 범위 안
        {
            ChangeState(Global.EnemyFsm.Attack);
        }
        else
        {
            Move();                             // 공격 범위 밖
        }
    }

    public override void Attack()
    {
        /*
        // 프레임마다 Attack 함수로 들어오는 거 막아야 함
        if (isAttacking) return;

        base.Attack(); // isAttacking = true;

        switch (attackNum)
        {
            case 0:
                myAnim.SetTrigger("isAtk_3_1");
                break;
            case 1:
                myAnim.SetTrigger("isAtk_3_2");
                break;
            case 2:
                myAnim.SetTrigger("isAtk_3_3");
                break;
            case 3:
                myAnim.SetTrigger("isAtk_3_final");
                break;
        }
        */

        if (!isAttacking)
        {
            base.Attack();

            if (attackNum == 0)
            {
                myAnim.SetTrigger("isAtk_3_1");
            }
            else if (attackNum == 1)
            {
                myAnim.SetTrigger("isAtk_3_2");
            }
            else if (attackNum == 2)
            {
                myAnim.SetTrigger("isAtk_3_3");
            }
            else if (attackNum == 3)
            {
                myAnim.SetTrigger("isAtk_3_final");
            }
        }
    }

    public override void AttackAfter()   // 이벤트 함수
    {
        base.AttackAfter(); // isAttacking = false;
        attackNum++;

        if (attackNum == 4)
        {
            attackNum = 0;
            ChangeState(Global.EnemyFsm.Idle);
        }
    }
}