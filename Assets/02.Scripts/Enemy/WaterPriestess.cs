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
    public bool isSpecialAttacking = false; // isSuperArmor, 역시 수시로 이동하는 걸 막기 위한 변수
    public bool isSecondPhase = false;  // false일 때만 heal 가능(1회)
    public bool isAirAtk = false;       // 트리거에서 다시 변경함
    public bool isAirAttacking = false; // 수시로 이동하는 걸 막기 위한 변수

    [Header("공격 콤보")]
    public bool attackCombo = false;    // 콤보 여부(애니메이션)
    public int attackCount = 0;         // 콤보 계산용 카운트
    public float atkComboTime = 1f;       // 콤보 인정 시간

    [Header("확률")]
    public int randomNum = 0;   // spAtk용
    public int spAtkVariable = 35;

    public IEnumerator attackDelay = null;
    public IEnumerator hitDelay = null;
    public IEnumerator healDelay = null;

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
        if(getHit)
        {
            hitCount++;
            getHit = false;
        }
        if (!isDie)
        {
            CheckTransition();
        }

        {
            if (hitCount <= 1 && hitDelay == null)  // 처음 진입 시 코루틴 실행
            {
                delayTime = 10f;
                hitDelay = HitDelay(delayTime);
                StartCoroutine(hitDelay);
            }

            if (hitCount >= 3 && hitCombo)  // 10초 안에 hitCount >= 3이 되면
            {
                isSuperArmor = true;
                myAnim.SetBool("isSpecialAttack", isSuperArmor);
                isSpecialAttacking = true;
                Debug.Log("SuperArmor Mode On");
            }
        }

        if(!isSecondPhase && enemyHealth.health <= 30)  // 1페이즈
        {
            StartState(Global.EnemyFsm.Meditate);
        }

        if(isSecondPhase && enemyHealth.health < enemyHealth.initHealth / 2 && !isSpecialAttacking)   // 2페이즈
        {
            if(randomNum < spAtkVariable)
            {
                isSuperArmor = true;
                myAnim.SetBool("isSpecialAttack", isSuperArmor);
                isSpecialAttacking = true;
                Debug.Log("SuperArmor Mode On");
            }
        }

        // 원래 시야거리로 했다가 사각지대가 생겨서 바꿈
        if (isSecondPhase && enemyHealth.health < enemyHealth.initHealth / 2 && !DistanceDecision(attackDistance) && !isAirAttacking)   // 2페이즈
        {
            Vector2 dir = myTarget.transform.position - this.transform.position;

            transform.position = dir.x > 0 ? new Vector3(myTarget.transform.position.x - 1.5f, transform.position.y, transform.position.z)
                                            : new Vector3(myTarget.transform.position.x + 1.5f, transform.position.y, transform.position.z);

            FlipSprite();
            isAirAtk = true;
            myAnim.SetBool("isAirAttack", isAirAtk);
            isAirAttacking = true;
        }
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
            case Global.EnemyFsm.Meditate:
                {
                    isSecondPhase = true;   // *중복 체크하기
                    // 힐 할 때 순간이동(hp가 절반 이하이고, 2페이즈라서)이 되는 버그 때문에 위로 올림

                    healDelay = HealCoroutine(enemyHealth.initHealth, 3f);
                    StartCoroutine(healDelay);
                    enemyHealth.damagePercent = 0f;

                    myAnim.SetBool("isMeditate", isMeditating);   // 바로 들어와서 true 되나?
                    Debug.Log("isMeditate : " + isMeditating);
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

        if (isSecondPhase)
        {
            randomNum = Random.Range(0, 100);

            Debug.Log(randomNum);
        }
    }

    public void SpAtkAfter()
    {
        Debug.Log("SuperArmor Mode Off");

        hitCount = 0;
        isSuperArmor = false;
        myAnim.SetBool("isSpecialAttack", isSuperArmor);
        isSpecialAttacking = false;

        if (isSecondPhase)
        {
            randomNum = Random.Range(0, 100);

            Debug.Log(randomNum);
        }
    }

    public void AirAtkAfter()
    {
        isAirAtk = false;
        myAnim.SetBool("isAirAttack", isAirAtk);
        isAirAttacking = false;

        if (isSecondPhase)
        {
            randomNum = Random.Range(0, 100);

            Debug.Log(randomNum);
        }
    }

    public void HealAfter()
    {
        enemyHealth.damagePercent = 1f;
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
        hitCount = 0;
    }
}
