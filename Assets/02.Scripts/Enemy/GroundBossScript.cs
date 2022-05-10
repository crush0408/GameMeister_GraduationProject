using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundBossScript : BossBase
{
    Vector2 playerPos = Vector2.zero;

    private int randomNum;

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        myFsm = Global.EnemyFsm.Chase;
        speed = 8f;
        delayTime = 2f;
        sightDistance = 15f;    // 시야 범위
        attackDistance = 4f;  // 공격 범위
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

    public void FsmUpdate()
    {
        switch (myFsm)
        {
            case Global.EnemyFsm.None:
                break;
            case Global.EnemyFsm.Idle:
                break;
            case Global.EnemyFsm.Patrol:
                break;
            case Global.EnemyFsm.Heal:
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
            case Global.EnemyFsm.PatternMove:
                //Jump();
                //Debug.Log("Jump");
                
                //ChangeState(Global.EnemyFsm.Delay);
                break;
            case Global.EnemyFsm.Delay:
                if(!isAttacking)
                {
                    ChangeState(Global.EnemyFsm.PatternMove);
                }
                break;
            default:
                break;
        }
    }

    // 타겟(플레이어)와 에너미(나)의 거리 반환
    public float GapX()  // GapX(myTarget.transform.position, transform.position)
    {
        return Mathf.Abs(myTarget.transform.position.x - transform.position.x);
    }

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
        if (GapX() >= attackDistance)
        {
            Debug.Log("플레이어와의 거리 : " + GapX());
            Move();

            myAnim.SetBool("isRun", true);
        }
        else
        {
            Debug.Log("change to attack type");
            
            myAnim.SetBool("isRun", false);
            ChangeState(Global.EnemyFsm.Attack);
        }
    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log("State : " + myFsm);

        randomNum = Random.Range(0, 100);
        Debug.Log("Random Number : " + randomNum);
        if (randomNum < 30)
        {
            myAnim.SetTrigger("isAtk1");
        }
        else if (randomNum < 60)
        {
            myAnim.SetTrigger("isAtk2");
        }
        else if (randomNum < 90)
        {
            myAnim.SetTrigger("isAtk3");
        }
        else
        {
            Debug.Log("헤헤 아직 없음");
        }

        myAnim.SetTrigger("isAtk1");
    }

    public override void AttackAfter()
    {
        base.AttackAfter();
        Debug.Log("State : " + myFsm);

        if (GapX() >= attackDistance)
        {
            Debug.Log("멀어짐");
            ChangeState(Global.EnemyFsm.Chase);
        }
        else
        {
            StartCoroutine(AttackDelay(3f));
        }
    }

    public IEnumerator AttackDelay(float delay)
    {
        Debug.Log("코루틴 작동 확인용");

        yield return new WaitForSeconds(delay);
        ChangeState(Global.EnemyFsm.Attack);
    }

    public override void Jump()
    {
        base.Jump();
        // Jump
        // 현재 위치 에서 플레이어 위치까지 포물선을 그리면서 뛰게 만들기
        // 현재 Jump라는 트리거를 만들어둔 상태 (Animator에서는 연결 x)
        // Animation이 JumpUp과 JumpDown있는데, 올라갈때는 JumpUp이 실행되고 내려올때는 JumpDown이 실행되게
        // 그리고 지금 Ground보스 JumpUp 세팅이 이상함
        playerPos = myTarget.transform.position;
        startPos = transform.position;
        lastPos = playerPos;
        middlePos = new Vector2(Vector2.Distance(startPos, lastPos) / 2, jumpPower);

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
