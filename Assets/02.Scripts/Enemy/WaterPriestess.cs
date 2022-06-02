using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPriestess : BossBase
{
    [Header("피격 콤보")]
    public bool hitCombo = false;
    public int hitCount = 0;
    public float hitComboTime = 2f;

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
        sightDistance = 12f;    // 시야 범위
        attackDistance = 5f;    // 공격 범위
        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (!isDie)
        {
            CheckTransition();
        }

        /*
        if (getHit)
        {
            hitCount++;

            if (hitCount <= 1)  // 처음 진입 시
            {
                delayTime = 5f;
                hitDelay = HitDelay(delayTime);
                StartCoroutine(hitDelay);
            }

            if (hitCount >= 3 && hitCombo)  // 5초 안에 hitCount >= 3이 되면
            {
                isSuperArmor = true;
            }
        }
        */
    }

    private void CheckTransition()
    {
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                {
                    if (DistanceDecision(sightDistance) && delayCoroutine == null)
                    {
                        delayTime = 1f;
                        delayCoroutine = Delay(delayTime, Global.EnemyFsm.Chase);
                        StartCoroutine(delayCoroutine);
                    }
                    else
                    {
                        StartState(Global.EnemyFsm.Idle);
                    }
                }
                break;
            case Global.EnemyFsm.Chase:
                {
                    if (DistanceDecision(attackDistance))   // 공격 사거리 내
                    {
                        myAnim.SetBool("isChase", false);
                        StartState(Global.EnemyFsm.Attack);
                    }
                    else if (DistanceDecision(sightDistance))   // 시야 범위 내
                    {
                        StartState(Global.EnemyFsm.Chase);
                    }
                    else
                    {
                        myAnim.SetBool("isChase", false);
                        StartState(Global.EnemyFsm.Idle);
                    }
                }
                break;
            case Global.EnemyFsm.Attack:
                {
                    if (!isAttacking)   // 애니메이션에서 isAttacking = false로 변경해줌
                    {
                        StartState(Global.EnemyFsm.Chase);
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
                    Attack();
                }
                break;
        }
    }

    public override void Attack()
    {
        base.Attack();  // isAttacking = true
        myAnim.SetBool("isAttacking", isAttacking);

        attackCount++;

        if (attackCount <= 1)        // attackCount : 1일 때
        {
            delayTime = 4f; // 4초 동안 3번 공격해야 콤보가 됨( 2 2 3 )
            attackDelay = AttackDelay(delayTime);
            StartCoroutine(attackDelay);
        }
        else if (attackCount >= 3 && attackCombo)    // 3번째 공격 attackCount : 3
        {
            // attackCombo = true;
            myAnim.SetBool("isAttackCombo", attackCombo);
        }
        else if (attackCount >= 3 && !attackCombo)
        {
            myAnim.SetBool("isAttackCombo", attackCombo);

            attackCount = 1;

            delayTime = 4f; // 4초 동안 3번 공격해야 콤보가 됨( 2 2 3 )
            attackDelay = AttackDelay(delayTime);
            StartCoroutine(attackDelay);
        }
    }

    public override void AttackAfter()
    {
        base.AttackAfter(); // isAttacking = false;
        myAnim.SetBool("isAttacking", isAttacking);

        if (attackCount >= 3 && attackCombo)    // 3번째 공격 After 때
        {
            attackCombo = false;
            myAnim.SetBool("isAttackCombo", attackCombo);

            StopCoroutine(attackDelay);

            attackCount = 0;    // 콤보 카운트 초기화

            StartState(Global.EnemyFsm.Chase);
        }
    }

    public override void GetHitAfter()
    {
        base.GetHitAfter();
    }

    public IEnumerator Delay(float delay, Global.EnemyFsm enemyFsm)
    {
        yield return new WaitForSeconds(delay);
        StartState(enemyFsm);
        delayCoroutine = null;
    }

    public IEnumerator AttackDelay(float delay)
    {
        attackCombo = true;
        yield return new WaitForSeconds(delay);
        attackCombo = false;

        attackDelay = null;
    }

    public IEnumerator HitDelay(float delay)
    {
        hitCombo = true;
        yield return new WaitForSeconds(delay);
        hitCombo = false;

        hitDelay = null;
    }
}
