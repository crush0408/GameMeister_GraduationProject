using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPriestess : BossBase
{
    // public int hitCount;

    public IEnumerator delayCoroutine;

    public override void Init()
    {
        base.Init();
        myFsm = Global.EnemyFsm.Idle;
        myType = Global.EnemyType.Walking;
        speed = 6f;

        sightDistance = 10f;    // 시야 범위
        attackDistance = 6f;    // 공격 범위
        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
    }

    private void Start()
    {
        Init();
    }

    private void CheckTransition()
    {
        if (getHit)
        {
            StartState(Global.EnemyFsm.GetHit);
            return;
        }

        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                {
                    if (delayCoroutine == null)
                    {
                        delayTime = Random.Range(1.4f, 2.0f);
                        delayCoroutine = Delay(delayTime, Global.EnemyFsm.Chase);
                        StartCoroutine(delayCoroutine);
                    }
                }
                break;
            case Global.EnemyFsm.Chase:
                {
                    if (DistanceDecision(attackDistance))
                    {
                        StartState(Global.EnemyFsm.Attack);
                    }
                }
                break;
            case Global.EnemyFsm.Attack:
                {
                    if (!isAttacking)
                    {
                        StartState(Global.EnemyFsm.Chase);
                    }
                }
                break;
            case Global.EnemyFsm.GetHit:
                {
                    if (!getHit)
                    {
                        StartState(Global.EnemyFsm.Idle);
                    }
                }
                break;
        }
    }

    private void StartState(Global.EnemyFsm state)
    {
        ChangeState(state);
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                {

                }
                break;
            case Global.EnemyFsm.Chase:
                {

                }
                break;
            case Global.EnemyFsm.Attack:
                {
                    Attack();       // 확인하기
                }
                break;
            case Global.EnemyFsm.GetHit:
                {
                    GetHit();
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

    public IEnumerator Delay(float delay, Global.EnemyFsm enemyFsm)
    {
        yield return new WaitForSeconds(delay);
        StartState(enemyFsm);
        delayCoroutine = null;
    }
}
