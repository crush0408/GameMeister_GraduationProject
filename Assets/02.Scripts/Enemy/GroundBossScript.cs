using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundBossScript : BossBase
{
    Vector2 playerPos = Vector2.zero;

    private Vector3 floatPos = Vector3.zero;
    private Vector3 fallDownPos = Vector3.zero;

    private bool isJumpAttackBefore = false;
    private bool isJumpAttack = false; // 변수명 어떠카지...

    private bool isHealing = false;
    private int randomNum = 0;
    private IEnumerator attackDelay;

    private float jumpAtkDist; // 점프 공격 사정거리

    private void Start()
    {
        Init();

        enemyHealth.HealHealth(-30);
        Debug.Log("적 초기 체력 : " + enemyHealth.health);
    }

    public override void Init()
    {
        base.Init();

        myFsm = Global.EnemyFsm.Chase;
        speed = 6f;
        delayTime = 2f; // 에너미 자동 공격 딜레이에 쓰겠음 
        sightDistance = 15f;    // 시야 범위
        attackDistance = 4f;  // 공격 범위
        jumpAtkDist = 6f;
        rightDirection = Vector3.one;   // 오른쪽 보고 시작
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
        jumpPower = 7f;
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
                /*
                if(enemyHealth.health <= 0)
                {
                    // Die();  // isDie 역시 이 안에서 처리됨
                }
                else
                {
                    ChangeState(Global.EnemyFsm.Chase);
                }
                */
                break;
            case Global.EnemyFsm.Chase:
                Chase();
                break;
            case Global.EnemyFsm.Attack:
                Attack();
                break;
            case Global.EnemyFsm.AttackAfter:
                AttackAfter();
                break;
            case Global.EnemyFsm.JumpAttackBefore:
                JumpAttackBefore();
                break;
            case Global.EnemyFsm.JumpAttack:
                JumpAttack();
                break;
            case Global.EnemyFsm.Heal:
                // if ()    // 빠져나가는 조건
                if (healCoroutine == null)
                {
                    healCoroutine = HealCoroutine(10, 1);
                    StartCoroutine(healCoroutine);
                }
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

    // werqreq
    public void Chase()
    {
        // Debug.Log("FSM : " + myFsm);

        // 공격 중이 아닌 해당 상태에서 Chase 이전 HP 체크 후 힐 필요함
        if ((enemyHealth.health < 60) && !isHealing)
        {
            isHealing = true;
            healCoroutine = HealCoroutine(1f, 3f);
            StartCoroutine(healCoroutine);
        }

        if (DistanceDecision(attackDistance)) // 공격 사정거리 안일 때
        {
            Debug.Log("Attack 타입으로 바뀌어야 함");

            myAnim.SetBool("isRun", false);
            ChangeState(Global.EnemyFsm.Attack);
        }
        else if (DistanceDecision(jumpAtkDist + 0.5f)){   // 공격 사정거리 밖, 점프공격 사정거리 안
            // 정확히 맞을 수는 없으므로 계산에는 0.5f 더함

            // Debug.Log("점프 어택");
            ChangeState(Global.EnemyFsm.JumpAttackBefore);
        }
        else // 플레이어에게 다가가야 함
        {
            Debug.Log("플레이어와의 거리 : " + Mathf.Abs(myTarget.transform.position.x - transform.position.x));
            Move();

            myAnim.SetBool("isRun", true);
        }
    }


    public override void Attack()
    {
        // Debug.Log("FSM : " + myFsm);
        base.Attack();

        // 랜덤 넘버 구하기
        if (randomNum == 0)
        {
            randomNum = Random.Range(1, 90);
            Debug.Log("Random Number : " + randomNum);

            // 랜덤 공격 애니메이션
            if (randomNum <= 30)
            {
                myAnim.SetTrigger("isAtk1");
            }
            else if (randomNum <= 60)
            {
                myAnim.SetTrigger("isAtk2");
            }
            else // else if (randomNum <= 90)
            {
                myAnim.SetTrigger("isAtk3");
            }
        }
    }

    public override void AttackAfter()
    {
        ChangeState(Global.EnemyFsm.AttackAfter);
        randomNum = 0;

        // Debug.Log("FSM : " + myFsm);
        base.AttackAfter();

        if (attackDelay == null)
        {
            attackDelay = AttackDelay(delayTime);
            StartCoroutine(attackDelay);
        }
    }

    public IEnumerator JumpAttackDelay()
    {
        while (transform.position.y < floatPos.y - 1f)
        {
            transform.position = Vector3.Lerp(transform.position, floatPos, 0.2f);
            Debug.Log("여러번 " + "현재 : " + transform.position.y + " < " + floatPos.y);

            yield return null;
        }

        Debug.Log("상태 변경");

        // fallDownPos = myTarget.transform.position; // 플레이어를 계속 따라가면 이상하니 처음 한 번만 지정 (1번 저장 방식, 2번보다 플레이 쉬움)

        myRigid.bodyType = RigidbodyType2D.Static;  // 잠깐 고정시키기 (Freeze Y Pos 하는 함수를 모르겠음...) 역시 임시방편
        myAnim.SetBool("isFloatDelay", true);
        yield return new WaitForSeconds(2f);    // 공중에서 2초 딜레이

        // myRigid.bodyType = RigidbodyType2D.Dynamic; // 리지드바디 타입 되돌리기 -> 상태 변경 이후 코드에서 실행

        isJumpAttackBefore = false; // 어디다 둬야 할 지 모르겠어서 상태 변경 직전으로
        ChangeState(Global.EnemyFsm.JumpAttack);
    }

    public void JumpAttackBefore()  // 애니메이션 필요
    {
        if (!isJumpAttackBefore)   // 중복 실행 방지
        {
            isJumpAttackBefore = true;
            floatPos = transform.position + new Vector3(0f, 5f, 0f);  // 플로팅 할 만큼의 타겟 포스 지정
            // floatPos = (floatPos == Vector3.zero) ? transform.position + new Vector3(0f, 3.5f, 0f) : floatPos;

            Debug.Log("한 번만");
            // StartCoroutine(Float(5f, 10f));  // 공중부양하는 함수 만들기

            StartCoroutine(JumpAttackDelay());
        }
    }

    public void JumpAttack()    // 애니메이션 필요
    {
        if (!isJumpAttack)
        {
            isJumpAttack = true;

            myRigid.bodyType = RigidbodyType2D.Dynamic; // 리지드바디 타입 되돌리기(스태틱 풀기)
            myAnim.SetBool("isFloatDelay", false);

            fallDownPos = myTarget.transform.position; // 플레이어를 계속 따라가면 이상하니 처음 한 번만 지정(2번 저장 방식)
            // 2번 쓸 거면 Delay 이후 상태 변환 전에 플립 한 번 해주기 || 어색하면 딜레이를 1초 1초로 나눠서 사이에 플립 한 번 해주기
            // fallDownPos = (fallDownPos == Vector3.zero) ? myTarget.transform.position : fallDownPos;
        }

        if (Vector3.Distance(transform.position, fallDownPos) <= 1f)  // Mathf.Lerp 사용하니까 미세하게 올라가서 목표치를 못 넘기길래 임시 방편으로 판정 줄여둠
        {
            // 공격 코드
            Debug.Log("공격");
            // myAnim.Play("JDown");   // 수정 바람

            // 공격을 언제 끝내야 하는 거지...
        }
        else
        {
            // 이동
            transform.position = Vector3.Lerp(transform.position, fallDownPos, 0.05f);
            Debug.Log("현재 거리 : " + Vector3.Distance(transform.position, fallDownPos));
        }


        // transform.position = floatPos;
        Debug.Log("점프 어택 진입");
    }

    public void JumpAttackAfter()   // FallDown 애니메이션 이벤트 함수 (ArrayNum 2번)
    {
        isJumpAttack = false;
    }

    public IEnumerator AttackDelay(float delay)
    {
        Debug.Log("코루틴 진입");

        yield return new WaitForSeconds(delay);

        if (DistanceDecision(attackDistance))
        {
            attackDelay = null;
            ChangeState(Global.EnemyFsm.Attack);
        }
        else
        {
            attackDelay = null;
            ChangeState(Global.EnemyFsm.Chase);
        }
    }

    // Dead 애니메이션 종료 시 이벤트 함수
    public void DeadAfter() // Base로 빼야 하나?
    {
        Debug.Log(this.gameObject.name + " 죽었음");
        gameObject.SetActive(false);
    }

    public override void Jump()
    {
        base.Jump();
        //Jump
        // 현재 위치 에서 플레이어 위치까지 포물선을 그리면서 뛰게 만들기
        // 현재 Jump라는 트리거를 만들어둔 상태 (Animator에서는 연결 x)
        // Animation이 JumpUp과 JumpDown있는데, 올라갈때는 JumpUp이 실행되고 내려올때는 JumpDown이 실행되게
        // 그리고 지금 Ground보스 JumpUp 세팅이 이상함
        playerPos = myTarget.transform.position;
        startPos = transform.position;
        lastPos = playerPos;

        if (startPos.x < lastPos.x)
        {
            middlePos = new Vector2(-(Vector2.Distance(startPos, lastPos) / 2), jumpPower);

        }
        else
        {
            middlePos = new Vector2(Vector2.Distance(startPos, lastPos) / 2, jumpPower);
        }

        StartCoroutine(JumpCorutine());
    }

    IEnumerator JumpCorutine()
    {
        float time = 0f;
        while ((Vector2)transform.position != playerPos)
        {
            time += Time.deltaTime;
            transform.position = BezierCurve(time);
            yield return null;

        }
        yield return null;
    }

    Vector3 BezierCurve(float t)
    {
        Debug.Log(middlePos);
        Vector2 p1 = Vector2.Lerp(startPos, middlePos, t);
        Vector2 p2 = Vector2.Lerp(middlePos, lastPos, t);

        return Vector2.Lerp(p1, p2, t);
    }
}
