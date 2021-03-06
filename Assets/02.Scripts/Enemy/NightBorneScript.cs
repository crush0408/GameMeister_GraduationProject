using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBorneScript : BasicEnemyBase
{
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        myFsm = Global.EnemyFsm.Idle;
        myType = Global.EnemyType.Walking;
        speed = 5f;
        patrolCoolTime = 0.5f;
        sightDistance = 10f;
        attackDistance = 4.8f;
        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTransition();
    }

    private void CheckTransition()
    {
        if (getHit) { StartState(Global.EnemyFsm.GetHit); getHit = false; }
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                {
                    if (DistanceDecision(sightDistance)) { StartState(Global.EnemyFsm.Chase); }
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
                        CoroutineInitialization(patrolCoroutine);
                        isPatroling = false;
                        StartState(Global.EnemyFsm.Attack);
                    }
                    else if (DistanceDecision(sightDistance))
                    {
                        CoroutineInitialization(patrolCoroutine);
                        isPatroling = false;
                        StartState(Global.EnemyFsm.Chase);
                    }
                    else { StartState(Global.EnemyFsm.Patrol); }
                }
                break;
            case Global.EnemyFsm.Attack:
                {
                    if (!isAttacking) { StartState(Global.EnemyFsm.Idle); }
                }
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
                        speed = 5f;
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

    public override void AttackAfter()
    {
        base.AttackAfter();
    }

    public void testHit()
    {
        getHit = false;
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
