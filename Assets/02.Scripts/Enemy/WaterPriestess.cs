using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPriestess : BossBase
{
    [Header("피격 콤보")]
    public bool hitCombo = false;   // 타임 체크용
    public int hitCount = 0;
    public float hitComboTime = 2f;

    public bool isSecondPhase = false;  // 2페이즈 체크, false일 때만 heal 가능(1회)

    [Header("공격 상태 확인용 변수")]
    public bool isSpecialAttacking = false; // isSuperArmor, 역시 수시로 이동하는 걸 막기 위한 변수
    public bool isAirAttacking = false; // 수시로 이동하는 걸 막기 위한 변수

    [Header("공격 콤보")]
    public bool attackCombo = false;    // 콤보 여부(애니메이션)
    public int attackCount = 0;         // 콤보 계산용 카운트
    public float atkComboTime = 1f;       // 콤보 인정 시간

    [Header("확률 계산용")]
    public int randomNum = 0;   // spAtk용
    public int spAtkVariable = 20;

    [Header("콤보 체크용 딜레이 코루틴")]
    public IEnumerator hitDelay = null;
    public IEnumerator attackDelay = null;

    public Text phaseText;  // 시연 시 페이즈 확인용

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

        phaseText.text = "Phase 1";
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
            StartState(Global.EnemyFsm.SpecialAttack);
        }

        // 1페이즈에서 hp가 30 이하일 때 힐 후 2페이즈 진입하는 함수
        if (!isSecondPhase && enemyHealth.health <= 30)
        {
            isSecondPhase = true;
            phaseText.text = "Phase 2";
            StartState(Global.EnemyFsm.Meditate);
        }

        if(isSecondPhase && (enemyHealth.health < enemyHealth.initHealth / 2) && !isSpecialAttacking)   // 2페이즈
        {
            if(randomNum < spAtkVariable)
            {
                StartState(Global.EnemyFsm.SpecialAttack);
            }
        }

        // 공격 거리 밖이고 update이므로 airAttack이 실행 중이 아니면 airAttack 실행
        if (isSecondPhase && (enemyHealth.health < enemyHealth.initHealth / 2) && !DistanceDecision(attackDistance) && !isAirAttacking)   // 2페이즈
        {
            Vector2 dir = myTarget.transform.position - this.transform.position;

            transform.position = dir.x > 0 ? new Vector3(myTarget.transform.position.x - 1.5f, transform.position.y, transform.position.z)
                                            : new Vector3(myTarget.transform.position.x + 1.5f, transform.position.y, transform.position.z);

            FlipSprite();   // 조준했으므로 플레이어 방향 바라보기
            isAirAttacking = true;
            myAnim.SetBool("isAirAttack", isAirAttacking);
        }

        enemyHealth.damagePercent = (healCoroutine == null) ? 1f : enemyHealth.damagePercent;
        // HealCoroutine()을 virtual로 만들고 오버라이드해서 damagePercent를 대입하는 방법 ?
    }

    private void CheckTransition()
    {
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                {
                    if (DistanceDecision(sightDistance) && delayCoroutine == null)
                    {
                        StartState(Global.EnemyFsm.Chase);
                    }
                    else
                    {
                        StartState(Global.EnemyFsm.Idle);
                    }
                }
                break;
            case Global.EnemyFsm.Chase:
                {
                    if (DistanceDecision(attackDistance))
                    {
                        myAnim.SetBool("isChase", false);
                        StartState(Global.EnemyFsm.Attack);
                    }
                    else if (DistanceDecision(sightDistance))
                    {
                        StartState(Global.EnemyFsm.Chase);
                    }
                    else
                    {
                        myAnim.SetBool("isChase", true);
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
                    if (!isSpecialAttacking)    // 이렇게 하면 가끔 진입할 때 isSpecialAttacking이 false라 안 먹힐 수 있음 -> 어떻게 하지
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
                    Debug.Log("어택!");
                    Attack();
                }
                break;
            case Global.EnemyFsm.Meditate:
                {
                    Meditate(); // Heal 코루틴 작동
                }
                break;
            case Global.EnemyFsm.SpecialAttack:
                {
                    isSpecialAttacking = true;

                    myAnim.SetBool("isSpecialAttack", isSpecialAttacking);
                    Debug.Log("SuperArmor Mode On");
                }
                break;
        }
    }

    public void Meditate()
    {
        enemyHealth.damagePercent = 0f;

        healCoroutine = HealCoroutine(enemyHealth.initHealth, 3f);
        StartCoroutine(healCoroutine);

        myAnim.SetBool("isMeditate", isMeditating);
    }

    public override void Attack()
    {
        base.Attack();  // isAttacking = true, FlipSprite
        myAnim.SetBool("isAttacking", isAttacking);

        attackCount++;

        if (attackCount <= 1)        // attackCount : 1일 때
        {
            delayTime = 4f; // 4초 동안 3번 공격해야 콤보가 됨( 2 2 3 )
            attackDelay = ComboChecking(delayTime, true, false);
            StartCoroutine(attackDelay);
        }
        else if (attackCount >= 3 && attackCombo)    // 3번째 공격 attackCount : 3
        {
            myAnim.SetBool("isAttackCombo", attackCombo);

            if (!attackCombo)
            {
                attackCount = 1;

                delayTime = 4f;     // 4초 동안 3번 공격해야 콤보가 됨( 2 2 3 )
                attackDelay = ComboChecking(delayTime, true, false);
                StartCoroutine(attackDelay);
            }
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

        randomNum = isSecondPhase ? Random.Range(0, 100) : randomNum;
    }

    public void SpAtkAfter()
    {
        Debug.Log("SuperArmor Mode Off");

        hitCount = 0;   // 콤보의 결과인 스페셜 어택 끝났으므로 hitCount 초기화

        isSpecialAttacking = false;
        myAnim.SetBool("isSpecialAttack", isSpecialAttacking);

        randomNum = isSecondPhase ? Random.Range(0, 100) : randomNum;
    }

    public void AirAtkAfter()
    {
        isAirAttacking = false;
        myAnim.SetBool("isAirAttack", isAirAttacking);

        randomNum = isSecondPhase ? Random.Range(0, 100) : randomNum;
    }

    public void SetRandomNum()  // 공격마다 넣을 public 함수?
    {
        randomNum = isSecondPhase ? Random.Range(0, 100) : randomNum;
        Debug.Log(randomNum);
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
}
