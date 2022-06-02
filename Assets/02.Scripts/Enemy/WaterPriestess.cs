using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPriestess : BossBase
{
    [Header("피격 콤보")]
    public bool hitCombo = false;
    public int hitCount = 0;
    public float hitComboTime = 1f;

    public bool isSuperArmor = false;   // 3번 연속 피격 당했을 시 슈퍼아머 상태

    [Header("공격 콤보")]
    public bool attackCombo = false;    // 콤보 여부(애니메이션)
    public int attackCount = 0;         // 콤보 계산용 카운트
    public float atkComboTime = 1f;       // 콤보 인정 시간

    public IEnumerator attackDelay = null;
    public IEnumerator hitDelay = null;

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
        if (getHit)
        {
            StartState(Global.EnemyFsm.GetHit); // 여기부터 작업하기
        }

        if (!isDie)
        {
            CheckTransition();
        }
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
    public override void Attack()
    {
        if(attackDelay != null)
        {
            attackCount++;
            Debug.Log("맞은 수 : " + attackCount);

            StopCoroutine(attackDelay);
            attackDelay = null;

            attackDelay = AttackDelay(atkComboTime);
            StartCoroutine(attackDelay);
        }
        else
        {
            attackCount = 0;

            attackDelay = AttackDelay(atkComboTime);
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

    public override void GetHitAfter()
    {
        base.GetHitAfter();
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

    public IEnumerator HitDelay(float delay)    // hit 역시 같은 방식으로 구현하기
    {
        yield return new WaitForSeconds(delay);
        hitDelay = null;
    }
}
