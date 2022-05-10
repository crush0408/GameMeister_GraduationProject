using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundBossScript : BossBase
{
    Vector2 playerPos = Vector2.zero;

    int randomNum = 0;

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        myFsm = Global.EnemyFsm.Chase;
        speed = 3f;
        delayTime = 2f;
        sightDistance = 15f;    // 시야 범위
        attackDistance = 3f;  // 공격 범위
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
                ChangeState(Global.EnemyFsm.AttackAfter);
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

    public void Chase()
    {
        if (Mathf.Abs(myTarget.transform.position.x - transform.position.x) >= attackDistance)
        {
            Debug.Log("현재 거리 : " + Mathf.Abs(myTarget.transform.position.x - transform.position.x));
            StartCoroutine(Patrol(myTarget.transform.position.x - transform.position.x)); // 이거 코루틴 어디에 언제 어떻게 쓰는지 물어보기
            /*
            randomNum = Random.Range(1, 100);
            if (randomNum < 30)
            {
                myAnim.SetBool("isRoll", true);
            }
            else
            {
                myAnim.SetBool("isRun", true);
            }
            */
            myAnim.SetBool("isRun", true);
        }
        else
        {
            myAnim.SetBool("isRun", false);

            ChangeState(Global.EnemyFsm.Attack);
        }
    }

    public override void Attack()
    {
        base.Attack();
        myAnim.SetTrigger("isAtk1");

        if (Mathf.Abs(myTarget.transform.position.x - transform.position.x) >= attackDistance)
        {
            ChangeState(Global.EnemyFsm.Chase);
        }
    }

    public override void AttackAfter()
    {
        base.AttackAfter();
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
