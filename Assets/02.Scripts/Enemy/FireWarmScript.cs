using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWarmScript : BasicEnemyBase
{
    public GameObject fireBall;
    public Transform firePosition;
    // Start is called before the first frame update
    public bool isDelay = false;
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        myFsm = Global.EnemyFsm.Idle;
        myType = Global.EnemyType.Walking;
        speed = 3f;
        patrolCoolTime = 0.5f;
        sightDistance = 12f;
        attackDistance = 2.5f;
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
        if (getHit) { StartState(Global.EnemyFsm.GetHit); return; }
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

        Instantiate(fireBall, firePosition);
    }

    public IEnumerator Delay(float delay)
    {
        isDelay = true;
        yield return new WaitForSeconds(delay);
        isDelay = false;
    }
}
