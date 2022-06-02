using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPriestess : BossBase
{
    // public int hitCount;

    public bool attackCombo = false;
    public int attackCount = 0;
    public float comboDelay = 2f;

    public IEnumerator attackDelay = null;

    public override void Init()
    {
        base.Init();
        myFsm = Global.EnemyFsm.Idle;
        myType = Global.EnemyType.Walking;      // 적 타입 : 지상 에너미
        speed = 3f;             // Idle(Standby) : 3f, Chase : 6f
        sightDistance = 10f;    // 시야 범위
        attackDistance = 2f;    // 공격 범위
        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        CheckTransition();
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
                        delayTime = 1.5f;
                        delayCoroutine = Delay(delayTime, Global.EnemyFsm.Chase);
                        StartCoroutine(delayCoroutine);
                    }
                    else
                    {
                        StartState(Global.EnemyFsm.Idle);
                    }
                }
                break;
            case Global.EnemyFsm.Chase: // Chase -> Idle은 불가능하도록 함
                {
                    if (DistanceDecision(attackDistance))
                    {
                        myAnim.SetBool("isChase", false);
                        StartState(Global.EnemyFsm.Attack);
                    }
                    else
                    {
                        StartState(Global.EnemyFsm.Chase);
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
                    speed = 3f;
                    Chase();
                }
                break;
            case Global.EnemyFsm.Chase:
                {
                    speed = 6f;
                    Chase();
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

    /*
    public override void Chase()
    {
        base.Chase();
        // speed = 6f;
    }
    */

    public override void Attack()
    {
        /*
        if (attackCombo)
        {
            attackCount++;
            Debug.Log("맞은 수 : " + attackCount);
        }
        StartCoroutine(AttackDelay(2f));
        */

        if(attackDelay != null)
        {
            attackCount++;
            Debug.Log("맞은 수 : " + attackCount);

            StopCoroutine(attackDelay);
            attackDelay = null;

            attackDelay = AttackDelay(comboDelay);
            StartCoroutine(attackDelay);
        }
        else
        {
            attackCount = 0;

            attackDelay = AttackDelay(comboDelay);
            StartCoroutine(attackDelay);
        }

        base.Attack();  // isAttacking = true;
        myAnim.SetBool("isAttacking", isAttacking);

        if(attackCount >= 2)
        {
            attackCombo = true;
        }
        else
        {
            attackCombo = false;
        }
        myAnim.SetBool("isAttackCombo", attackCombo);
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

    public IEnumerator AttackDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackDelay = null;
    }
}
