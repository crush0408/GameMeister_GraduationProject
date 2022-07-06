using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWarmScript : BasicEnemyBase
{
    public GameObject fireBall; //불 프리팹
    public Transform firePosition; //불이 나오는 입 포지션
    public bool isDelay = false; //딜레이 중인가?
    void Start()
    {
        Init(); //값 세팅
    }

    public override void Init()
    {
        base.Init();
        myFsm = Global.EnemyFsm.Idle;
        myType = Global.EnemyType.Walking;
        speed = 3f;
        patrolCoolTime = 0.5f;
        sightDistance = 12f;
        attackDistance = 7f;
        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTransition(); //어떤 상태인지 실행
    }

    private void CheckTransition()
    {
        if (getHit) { StartState(Global.EnemyFsm.GetHit); return; }
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                {
                    if (DistanceDecision(sightDistance) && !isDelay) { StartState(Global.EnemyFsm.Chase); }
                    else if (isDelay) { return; }
                    else { StartState(Global.EnemyFsm.Patrol); }
                }
                break;
            case Global.EnemyFsm.Chase:
                {
                    if (DistanceDecision(attackDistance)) { StartState(Global.EnemyFsm.Attack); }
                    else if (!DistanceDecision(sightDistance)) { StartState(Global.EnemyFsm.Idle); }
                    else { StartState(Global.EnemyFsm.Chase); }
                }
                break;
            case Global.EnemyFsm.Patrol:
                {
                    if (DistanceDecision(attackDistance))
                    {
                        StopCoroutine(patrolCoroutine); patrolCoroutine = null;
                        isPatroling = false; StartState(Global.EnemyFsm.Attack);
                    }
                    else if (DistanceDecision(sightDistance))
                    {
                        StopCoroutine(patrolCoroutine); patrolCoroutine = null;
                        isPatroling = false; StartState(Global.EnemyFsm.Chase);
                    }
                    else { StartState(Global.EnemyFsm.Patrol); }
                }
                break;
            case Global.EnemyFsm.Attack:
                if (!isAttacking) { StartState(Global.EnemyFsm.Idle); }
                break;
            case Global.EnemyFsm.GetHit:
                {
                    if (!getHit) { StartState(Global.EnemyFsm.Idle); }
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
                    Stop();
                    FlipSprite();
                    
                }
                break;
            case Global.EnemyFsm.Chase:
                {
                    speed = 5f;
                    Chase();
                }
                break;
            case Global.EnemyFsm.Patrol:
                {
                    if (patrolCoroutine == null)
                    {
                        speed = 3f;
                        patrolCoroutine = Patrol();
                        StartCoroutine(patrolCoroutine);
                    }
                    else
                    {

                        myRigid.velocity = myVelocity;
                    }
                }
                break;
            case Global.EnemyFsm.Attack:
                {
                    Attack();
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
        base.Attack();
        StopAllCoroutines();
        StartCoroutine(Delay(2));
    }

    public IEnumerator Delay(float delay)
    {
        Instantiate(fireBall, firePosition);

        yield return new WaitForSeconds(delay);
        StartState(Global.EnemyFsm.Idle);

        //while (!isDelay)
        //{
        //    isDelay = true;

        //    Instantiate(fireBall, firePosition);
        //    myAnim.SetBool("isAttacking", false);
        //    isDelay = false;
        //    yield return new WaitForSeconds(delay);
        //}

    }
    public override void AttackAfter()
    {
        base.AttackAfter();
    }
    public override void DeadAnimScript()
    {
        base.DeadAnimScript();
        GameManager.instance.AddCoin(Random.Range(3, 6));
    }

    public override void GetHitAfter()
    {
        base.GetHitAfter();
    }
}
