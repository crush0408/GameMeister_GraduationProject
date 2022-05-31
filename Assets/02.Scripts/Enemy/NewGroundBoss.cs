using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGroundBoss : BossBase
{

    public IEnumerator delayCoroutine;
    public IEnumerator defendCoroutine;

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
        delayCoroutine = null;
        defendCoroutine = null;
        hitCount = 0;
        myFsm = Global.EnemyFsm.Idle;
        speed = 6f;
        delayTime = 1.5f;
        sightDistance = 10f;    // 시야 범위
        attackDistance = 2f;  // 공격 범위
        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
        isSpecial = false;
    }
    /*
    private void Update()
    {
        if (enemyHealth.health / enemyHealth.initHealth < 0.4f && !isSpecial)
        {
            isSpecial = true;
            if (isMeditating)
            {
                StopCoroutine(healCoroutine);
                healCoroutine = null;
                enemyHealth.HealHealth(5);
                PoolableMono poolingObject = PoolManager.Instance.Pop("HealEffect");
                poolingObject.transform.parent = this.transform;
                poolingObject.transform.localPosition = new Vector3(0, 0, 0);
                isMeditating = false;
                myAnim.SetBool("isMeditate", isMeditating);
            }
            myAnim.SetTrigger("Special");
            myAnim.SetBool("isSpecial", isSpecial);
            if (delayCoroutine != null)
            {
                StopCoroutine(delayCoroutine);
                delayCoroutine = null;
            }
            ChangeState(Global.EnemyFsm.PatternMove);
        }
        
        if(getHit)
        {
            if(!isMeditating && !isSpecial)
            {
                hitCount++;
                if (hitCount >= 3)
                {
                    myAnim.SetBool("isAttacking", false);
                    isAttacking = false;
                    if(delayCoroutine != null)
                    {
                        StopCoroutine(delayCoroutine);
                        delayCoroutine = null;
                    }
                    ChangeState(Global.EnemyFsm.Meditate);
                    hitCount = 0;
                }
                
            }
            getHit = false;
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
                    delayTime = Random.Range(1.4f, 2f);
                    delayCoroutine = Delay(delayTime, Global.EnemyFsm.Chase);
                    StartCoroutine(delayCoroutine);
                }
                break;
            case Global.EnemyFsm.Chase:
                if(DistanceDecision(attackDistance))
                {
                    ChangeState(Global.EnemyFsm.Attack);
                }
                Chase();
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
                
                if (!isAttacking)
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
                
                myAnim.Play("Defend");
                ChangeState(Global.EnemyFsm.PatternDelay);

                break;
            case Global.EnemyFsm.PatternDelay:
                FlipSprite();
                if (defendCoroutine == null)
                {
                    defendCoroutine = Defend();
                    StartCoroutine(defendCoroutine);
                }
                break;
        }
    }
    public void SpecialAtkMove()
    {
        Vector2 dir = myTarget.transform.position - this.transform.position;

        if(dir.x > 0)
        {
            transform.position =
                new Vector3(myTarget.transform.position.x + 1.5f
                , transform.position.y,
                transform.position.z);
        }
        else
        {
            transform.position =
                new Vector3(myTarget.transform.position.x - 1.5f
                , transform.position.y,
                transform.position.z);
        }
        FlipSprite();
    }
    // 타겟(플레이어) 위치로 이동
    

    public IEnumerator Delay(float delay, Global.EnemyFsm enemyFsm)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(enemyFsm);
        delayCoroutine = null;
    }
    public IEnumerator Defend()
    {
        yield return new WaitForSeconds(3f);
        ChangeState(Global.EnemyFsm.Attack);
        defendCoroutine = null;
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
        healCoroutine = HealCoroutine(5f, 3f);
        StartCoroutine(healCoroutine);
    }

    public override void AttackAfter()   // 이벤트 함수
    {
        base.AttackAfter();
    }
    public override void DeadAnimScript()
    {
        base.DeadAnimScript();
    }
    */
}
