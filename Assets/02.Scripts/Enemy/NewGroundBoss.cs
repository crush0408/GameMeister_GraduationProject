using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGroundBoss : BossBase
{
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
        isDelay = false;
        delayCoroutine = null;
        defendCoroutine = null;
        hitCount = 0;
        myFsm = Global.EnemyFsm.Idle;
        StartState(Global.EnemyFsm.Idle);
        speed = 6f;
        sightDistance = 10f;    // 시야 범위
        attackDistance = 2f;  // 공격 범위
        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
        isSpecial = false;
        healAmount = 5f;
    }
    
    private void Update()
    {
        if (enemyHealth.health / enemyHealth.initHealth < 0.4f && !isSpecial)
        {
            isSpecial = true;
            if (isMeditating)
            {
                CoroutineInitialization(healCoroutine);
                enemyHealth.HealHealth(healAmount);

                PoolableMono poolingObject = PoolManager.Instance.Pop("HealEffect");
                poolingObject.transform.parent = this.transform;
                poolingObject.transform.localPosition = Vector3.zero;
                isMeditating = false;
                myAnim.SetBool("isMeditate", isMeditating);
            }

            myAnim.SetTrigger("Special");
            myAnim.SetBool("isSpecial", isSpecial);

            if (delayCoroutine != null)
            {
                CoroutineInitialization(delayCoroutine);
                isDelay = false;
            }
            StartState(Global.EnemyFsm.Pattern);
            myAnim.Play("Defend");
        }
        if (getHit)
        {
            if (!isMeditating && !isSpecial)
            {
                hitCount++;
                if (hitCount >= 3)
                {
                    myAnim.SetBool("isAttacking", false);
                    isAttacking = false;

                    if (delayCoroutine != null)
                    {
                        CoroutineInitialization(delayCoroutine);
                        isDelay = false;
                    }
                    StartState(Global.EnemyFsm.Meditate);
                    hitCount = 0;
                }
            }
            getHit = false;
        }
        if (!isDie)
        {
            CheckTransition();
        }
    }
    private void CheckTransition()
    {
        
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                {
                    if (!isDelay) { StartState(Global.EnemyFsm.Chase); }
                }
                break;
            case Global.EnemyFsm.Chase:
                {
                    if(DistanceDecision(attackDistance)) { StartState(Global.EnemyFsm.Attack); }
                    else { StartState(Global.EnemyFsm.Chase); }
                }
                break;
            case Global.EnemyFsm.Attack:
                {
                    if(!isAttacking) { StartState(Global.EnemyFsm.Idle); }
                }
                break;
            case Global.EnemyFsm.Meditate:
                {
                    if(!isMeditating) 
                    {
                        StartState(Global.EnemyFsm.Chase);
                    }
                }
                break;
            case Global.EnemyFsm.Pattern:
                {
                    FlipSprite();
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
                    delayTime = Random.Range(1.4f, 2f);
                    delayCoroutine = Delay(delayTime);
                    StartCoroutine(delayCoroutine);
                }
                break;
            case Global.EnemyFsm.Chase:
                {
                    Chase();
                }
                break;
            case Global.EnemyFsm.Attack:
                {
                    Attack();
                }
                break;
            case Global.EnemyFsm.Meditate:
                {
                    isMeditating = true;
                    myAnim.SetBool("isMeditate", true);
                }
                break;
            case Global.EnemyFsm.Pattern:
                {
                    defendCoroutine = Defend();
                    StartCoroutine(defendCoroutine);
                    enemyHealth.damagePercent = 0.3f;
                }
                break;
        }
    }


    public void SpecialAtkMove() // 타겟(플레이어) 위치로 이동
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
    
    public IEnumerator Delay(float delay)
    {
        isDelay = true;
        yield return new WaitForSeconds(delay);
        isDelay = false;
        delayCoroutine = null;
    }
    public IEnumerator Defend()
    {
        yield return new WaitForSeconds(3f);
        defendCoroutine = null;
        StartState(Global.EnemyFsm.Attack);
        enemyHealth.damagePercent = 1f;
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
        healCoroutine = HealCoroutine(healAmount, 3f);
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
}
