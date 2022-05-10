using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundBossScript : BossBase
{
    Vector2 playerPos = Vector2.zero;

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
        jumpAtkDist = 8f;
        rightDirection = Vector3.one;   // 오른쪽 보고 시작
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
        jumpPower = 7f;
    }

    private void Update()
    {
        if(!isDie)
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
            case Global.EnemyFsm.Chase:
                Chase();
                break;
            case Global.EnemyFsm.Attack:
                Attack();
                break;
            case Global.EnemyFsm.AttackAfter:
                AttackAfter();
                break;
            case Global.EnemyFsm.Heal:
                break;
            default:
                break;
        }
    }

    // 타겟(플레이어)와 에너미(나)의 거리 반환
    public float XPosGap()  // GapX(myTarget.transform.position, transform.position)
    {
        return Mathf.Abs(myTarget.transform.position.x - transform.position.x);
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

        if (XPosGap() < attackDistance) // 공격 사정거리 안일 때
        {
            Debug.Log("Attack 타입으로 바뀌어야 함");

            myAnim.SetBool("isRun", false);
            ChangeState(Global.EnemyFsm.Attack);
        }
        else if (XPosGap() < jumpAtkDist)   // 공격 사정거리 밖, 점프공격 사정거리 안
        {
            // Debug.Log("점프 어택");
        }
        else // 플레이어에게 다가가야 함
        {
            // Debug.Log("플레이어와의 거리 : " + XPosGap());
            Move();

            myAnim.SetBool("isRun", true);
        }
    }

    public override void Attack()
    {
        // Debug.Log("FSM : " + myFsm);
        base.Attack();

        // 랜덤 넘버 구하기
        if(randomNum == 0)
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
            attackDelay = AttackDelay(delayTime   );
            StartCoroutine(attackDelay);
        }
    }

    public IEnumerator AttackDelay(float delay)
    {
        Debug.Log("코루틴 진입");

        yield return new WaitForSeconds(delay);

        if (XPosGap() <= attackDistance)
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
