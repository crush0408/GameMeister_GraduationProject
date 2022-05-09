using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundBossScript : BossBase
{
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        myFsm = Global.EnemyFsm.PatternMove;
        speed = 3f;
        delayTime = 2f;
        sightDistance = 12f;
        attackDistance = 2.5f;
        rightDirection = Vector3.one;
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
                break;
            case Global.EnemyFsm.Attack:
                break;
            case Global.EnemyFsm.AttackAfter:
                break;
            case Global.EnemyFsm.PatternMove:
                Jump();
                Debug.Log("Jump");
                
                ChangeState(Global.EnemyFsm.Delay);
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
    public override void Jump()
    {
        base.Jump();
        //Jump
        // 현재 위치 에서 플레이어 위치까지 포물선을 그리면서 뛰게 만들기
        // 현재 Jump라는 트리거를 만들어둔 상태 (Animator에서는 연결 x)
        // Animation이 JumpUp과 JumpDown있는데, 올라갈때는 JumpUp이 실행되고 내려올때는 JumpDown이 실행되게
        // 그리고 지금 Ground보스 JumpUp 세팅이 이상함

        //myRigid.velocity = new Vector2(myRigid.velocity.x, jumpForce); 걍 위로 점프

        //애니메이션이 안돼요
        //myAnim.SetFloat("tree", myRigid.velocity.y);
        //Debug.Log(myRigid.velocity.y);


        Vector3 startPos = transform.position;
        Vector3 lastPos = myTarget.transform.position;
        Vector3 middlePos;
        if (transform.position.x < myTarget.transform.position.x)
        {
            middlePos = startPos + new Vector3(Vector3.Distance(startPos, lastPos) / 2, jumpPower);
        }
        else
        {
            middlePos = startPos + new Vector3(-(Vector3.Distance(startPos, lastPos)) / 2, jumpPower);
        }
        transform.DOPath(new[] { middlePos, startPos + Vector3.up, middlePos + Vector3.left * 0.8f, lastPos + Vector3.up, middlePos + Vector3.right * 1, lastPos + Vector3.up }, 1f, PathType.CubicBezier).SetEase(Ease.Unset);
    }
}
