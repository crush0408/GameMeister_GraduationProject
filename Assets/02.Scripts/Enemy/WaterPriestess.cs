using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPriestess : BossBase
{
    // 테스트용
    public Global.EnemyFsm beforeFsm;

    [Header("피격 콤보")]
    public bool hitCombo = false;   // 타임 체크용
    public int hitCount = 0;
    public float hitComboTime = 2f;

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

    [Header("콤보 체크용 딜레이 코루틴")]
    public IEnumerator hitDelay = null;
    public IEnumerator attackDelay = null;
    public override void Init()
    {
        base.Init();
        myType = Global.EnemyType.Walking;      // 적 타입 : 지상 에너미
        myFsm = Global.EnemyFsm.Idle;

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
        if (!isDie) CheckTransition();

        if (getHit)
        {
            hitCount++;
            getHit = false;
        }

        if (hitCount > 0 && hitDelay == null)  // hitCount가 이제 쌓이기 시작하고 hitDelay 코루틴이 없다면(중복 실행 방지) 코루틴 실행
        {
            delayTime = 5f;
            hitDelay = ComboChecking(delayTime, false, true);
            StartCoroutine(hitDelay);
        }

        if (hitCount >= 3 && hitCombo && !isSpecialAttacking)  // 10초 안에 hitCount >= 3이 되면
        {
            beforeFsm = myFsm;
            StartState(Global.EnemyFsm.SpecialAttack);
        }

        if (!isSecondPhase && enemyHealth.health <= 30)  // 1페이즈
        {
            isSecondPhase = true;   // *중복 체크하기
                                    // 힐 할 때 순간이동(hp가 절반 이하이고, 2페이즈라서)이 되는 버그 때문에 위로 올림
            StartState(Global.EnemyFsm.Meditate);
        }

        if(isSecondPhase && enemyHealth.health < enemyHealth.initHealth / 2 && !isSpecialAttacking)   // 2페이즈
        {
            if(randomNum < spAtkVariable)
            {
                beforeFsm = myFsm;
                StartState(Global.EnemyFsm.SpecialAttack);
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
            SetAnim("isAirAttack", isAirAtk);
            isAirAttacking = true;
        }

        Debug.Log("현재 상태 " + myFsm);

        if (healCoroutine == null)
        {
            enemyHealth.damagePercent = 1f;
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
                        delayCoroutine = StateChangeDelay(delayTime, Global.EnemyFsm.Chase);
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
                    if (DistanceDecision(attackDistance))       // 공격 사거리 내
                    {
                        SetAnim("isChase", false);
                        StartState(Global.EnemyFsm.Attack);
                    }
                    else if (DistanceDecision(sightDistance))   // 시야 범위 내
                    {
                        StartState(Global.EnemyFsm.Chase);
                    }
                    else
                    {
                        SetAnim("isChase", false);
                        StartState(Global.EnemyFsm.Idle);
                    }
                }
                break;
            case Global.EnemyFsm.Attack:
                {
                    if (!isAttacking)   // 공격이 끝나면 애니메이션 이벤트 함수에서 isAttacking = false로 변경해줌
                    {
                        StartState(Global.EnemyFsm.Idle);   // Chase였는데 Idle로 바꿈
                    }
                }
                break;
            case Global.EnemyFsm.Meditate:
                {

                }
                break;
            case Global.EnemyFsm.SpecialAttack:
                {
                    if (!isSpecialAttacking)
                    {
                        StartState(beforeFsm);
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
                    enemyHealth.damagePercent = 0f;
                    Meditate(); // Heal 코루틴 작동
                }
                break;
            case Global.EnemyFsm.SpecialAttack:
                {
                    isSpecialAttacking = true;

                    SetAnim("isSpecialAttack", isSpecialAttacking);
                    Debug.Log("SuperArmor Mode On");
                }
                break;
        }
    }

    public void Meditate()
    {
        healCoroutine = HealCoroutine(enemyHealth.initHealth, 4f);
        StartCoroutine(healCoroutine);

        SetAnim("isMeditate", isMeditating);
    }

    public override void Attack()
    {
        base.Attack();  // isAttacking = true
        SetAnim("isAttacking", isAttacking);

        attackCount++;

        if (attackCount <= 1)        // attackCount : 1일 때
        {
            delayTime = 4f; // 4초 동안 3번 공격해야 콤보가 됨( 2 2 3 )
            attackDelay = ComboChecking(delayTime, true, false);
            StartCoroutine(attackDelay);
        }
        else if (attackCount >= 3 && attackCombo)    // 3번째 공격 attackCount : 3
        {
            SetAnim("isAttackCombo", attackCombo);
        }
        else if (attackCount >= 3 && !attackCombo)
        {
            SetAnim("isAttackCombo", attackCombo);

            attackCount = 1;

            delayTime = 4f; // 4초 동안 3번 공격해야 콤보가 됨( 2 2 3 )
            attackDelay = ComboChecking(delayTime, true, false);
            StartCoroutine(attackDelay);
        }
    }

    public override void AttackAfter()
    {
        base.AttackAfter(); // isAttacking = false;
        SetAnim("isAttacking", isAttacking);

        if (attackCount >= 3 && attackCombo)    // 3번째 공격 After 때
        {
            attackCombo = false;
            SetAnim("isAttackCombo", attackCombo);

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
        SetAnim("isSpecialAttack", isSpecialAttacking);

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
        SetAnim("isAirAttack", isAirAtk);
        isAirAttacking = false;

        if (isSecondPhase)
        {
            randomNum = Random.Range(0, 100);

            Debug.Log(randomNum);
        }
    }

    public IEnumerator StateChangeDelay(float delay, Global.EnemyFsm enemyFsm)
    {
        yield return new WaitForSeconds(delay);
        StartState(enemyFsm);
        delayCoroutine = null;
    }

    public IEnumerator ComboChecking(float delay, bool isAttackCombo, bool isHitCombo)
    {
        // 상태 진입 체크
        attackCombo = isAttackCombo ? true : attackCombo;
        hitCombo = isHitCombo ? true : hitCombo;

        yield return new WaitForSeconds(delay);

        // 상태 종료 체크
        attackDelay = isAttackCombo ? null : attackDelay;
        hitDelay = isHitCombo ? null : attackDelay;
        hitCount = isHitCombo ? 0 : hitCount; // delay 시간 동안 때린 횟수 초기화
    }

    public void SetAnim(string animName, bool setBool)
    {
        myAnim.SetBool(animName, setBool);
    }
}
