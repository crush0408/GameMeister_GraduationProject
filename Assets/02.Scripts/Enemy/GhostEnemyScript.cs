using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyScript : FlyingEnemyBase
{
    private float attackCount = 0f;
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        myFsm = Global.EnemyFsm.Idle;
        speed = 3f;
        delayTime = 2f;
        sightDistance = 12f;
        attackDistance = 2.5f;
        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
    }
    private void Update()
    {
        FsmUpdate();
    }
    private void FsmUpdate()
    {
        if(!isDie)
        {
            switch (myFsm)
            {
                
                case Global.EnemyFsm.None:
                    break;
                case Global.EnemyFsm.Idle:
                    if(getHit)
                    {
                        ChangeState(Global.EnemyFsm.GetHit);
                    }
                    else if(DistanceDecision(sightDistance))
                    {
                        ChangeState(Global.EnemyFsm.Chase);
                    }
                    else
                    {
                        base.Stop();
                        //ChangeState(Global.EnemyFsm.Patrol);
                    }
                    break;
                case Global.EnemyFsm.Patrol:
                    if (getHit)
                    {
                        ChangeState(Global.EnemyFsm.GetHit);
                    }
                    else if (DistanceDecision(attackDistance))
                    {
                        ChangeState(Global.EnemyFsm.Attack);
                    }
                    else if(DistanceDecision(sightDistance))
                    {
                        ChangeState(Global.EnemyFsm.Chase);
                    }
                    if(patrolCoroutine == null)
                    {
                        float random = Random.Range(-1, 2);
                        patrolCoroutine = Patrol(random);
                        StartCoroutine(patrolCoroutine);
                    }
                    
                    break;
                case Global.EnemyFsm.Meditate:
                    break;
                case Global.EnemyFsm.Chase:
                    if (getHit)
                    {
                        ChangeState(Global.EnemyFsm.GetHit);
                    }
                    else if(!DistanceDecision(sightDistance))
                    {
                        ChangeState(Global.EnemyFsm.Idle);
                    }
                    else if (DistanceDecision(attackDistance))
                    {
                        ChangeState(Global.EnemyFsm.Attack);
                    }
                    base.Flying();
                    break;
                case Global.EnemyFsm.Attack:
                    if (getHit)
                    {
                        ChangeState(Global.EnemyFsm.GetHit);
                    }
                    Attack();
                    ChangeState(Global.EnemyFsm.AttackAfter);
                    break;
                case Global.EnemyFsm.AttackAfter:
                    if (getHit)
                    {
                        ChangeState(Global.EnemyFsm.GetHit);
                    }
                    ChangeState(Global.EnemyFsm.Delay);
                    break;
                case Global.EnemyFsm.GetHit:
                    base.GetHit();
                    ChangeState(Global.EnemyFsm.GetHitAfter);
                    break;
                case Global.EnemyFsm.GetHitAfter:
                    if(!getHit)
                    {
                        ChangeState(Global.EnemyFsm.Idle);
                    }
                    break;
                case Global.EnemyFsm.Delay:
                    // Do Not Any Action
                    // Only Transition
                    if (getHit)
                    {
                        ChangeState(Global.EnemyFsm.GetHit);
                    }

                    if (!isAttacking)
                    {
                        ChangeState(Global.EnemyFsm.Idle);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    

    
    public override void Attack()
    {
        base.Attack();
        if (enemyHealth.health / enemyHealth.initHealth > 0.6f)
        {
            if (attackCount % 2 == 0)
            {
                // 짝수
                myAnim.SetTrigger("isAttackTwo");
            }
            else
            {
                // 홀수
                myAnim.SetTrigger("isAttack");

            }
        }
        else
        {
            myAnim.SetTrigger("isAttackThree");
        }
        attackCount++;
    }
    public override void AttackAfter()
    {
        base.AttackAfter();
    }
    public override void DeadAnimScript()
    {
        base.DeadAnimScript();
    }
    public override void GetHitAfter()
    {
        base.GetHitAfter();
    }
}