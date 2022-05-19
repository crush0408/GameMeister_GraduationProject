using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGroundBoss : BossBase
{

    public IEnumerator delayCoroutine;

    public Transform[] healTrm;

    public int hitCount;

    public bool isSpecial;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        myFsm = Global.EnemyFsm.Idle;
        speed = 6f;
        delayTime = 1.5f;
        sightDistance = 10f;    // 시야 범위
        attackDistance = 2f;  // 공격 범위
        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
        isSpecial = false;
    }

    private void Update()
    {
        if(enemyHealth.health / enemyHealth.initHealth < 0.4f && !isSpecial)
        {
            isSpecial = true;
            ChangeState(Global.EnemyFsm.PatternMove);
        }
        if(!isMeditating)
        {
            if (getHit)
            {
                hitCount++;
                getHit = false;
            }
            if(hitCount >= 3)
            {
                myAnim.SetBool("isAttacking", false);
                isAttacking = false;
                ChangeState(Global.EnemyFsm.Meditate);
                hitCount = 0;
            }
        }
        if (!isDie)
        {
            FsmUpdate();
        }
    }

    public void FsmUpdate() // 매 프레임마다 실행
    {
        

        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                if(delayCoroutine == null)
                {
                    delayTime = Random.Range(0.9f, 1.5f);
                    delayCoroutine = Delay(delayTime, Global.EnemyFsm.Chase);
                    StartCoroutine(delayCoroutine);
                }
                break;
            case Global.EnemyFsm.Chase:
                if(DistanceDecision(attackDistance))
                {
                    ChangeState(Global.EnemyFsm.Attack);
                }
                Move();
                break;
            case Global.EnemyFsm.Attack:
                Attack();
                ChangeState(Global.EnemyFsm.AttackAfter);
                break;
            case Global.EnemyFsm.AttackAfter:
                if(!isAttacking)
                {
                    ChangeState(Global.EnemyFsm.Idle);
                }
                break;
            case Global.EnemyFsm.Meditate:
                if(!isMeditating && !isAttacking)
                {
                    myAnim.SetBool("isMeditate", true);
                    
                }
                break;
            case Global.EnemyFsm.Delay:
                if(!isMeditating)
                {
                    ChangeState(Global.EnemyFsm.Idle);
                }
                break;
            case Global.EnemyFsm.PatternMove:
                
                break;
        }
    }
    public void SpecialAtk()
    {

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

    public IEnumerator Delay(float delay, Global.EnemyFsm enemyFsm)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(enemyFsm);
        delayCoroutine = null;
    }


    public override void Attack()
    {
        base.Attack();
        myAnim.SetTrigger("Attack");
        myAnim.SetBool("isAttacking", true);
    }

    public void AttackEnd()
    {
        myAnim.SetBool("isAttacking", false);
        isAttacking = false;
    }
    public void GoMeditate()
    {
        int random = Random.Range(0, 2);
        transform.position = healTrm[random].position;
        //myAnim.Play("Meditate");
        healCoroutine = HealCoroutine(20f, 10f);
        StartCoroutine(healCoroutine);
        ChangeState(Global.EnemyFsm.Delay);
    }

    public override void AttackAfter()   // 이벤트 함수
    {
        base.AttackAfter();
    }
}
