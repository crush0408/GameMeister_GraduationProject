using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastDeathAngelScript : BasicEnemyBase
{
    public IEnumerator delayCoroutine;
    public bool isDelay = false;
    public float delayTime = 0f;

    public GameObject skillPrefab;
    public Transform skillPos;
    public IEnumerator Delay(float delay)
    {
        isDelay = true;
        yield return new WaitForSeconds(delay);
        isDelay = false;
        delayCoroutine = null;
    }

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        myFsm = Global.EnemyFsm.Idle;
        myType = Global.EnemyType.Flying;

        delayTime = 3f;

        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
        StartState(Global.EnemyFsm.Idle);
    }
    private void Update()
    {
        if(!isDie)
        {
            CheckTransition();
        }
    }
    private void CheckTransition()
    {
        if(getHit) 
        { 
            if(isDelay)
            {
                isDelay = false;
                CoroutineInitialization(delayCoroutine);
            }
            StartState(Global.EnemyFsm.GetHit);
            return; 
        }
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:
                {
                    FlipSprite();
                    if(!isDelay)
                    {
                        StartState(Global.EnemyFsm.Attack);
                    }
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
                    delayCoroutine = Delay(delayTime);
                    StartCoroutine(delayCoroutine);
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
        myAnim.SetTrigger("Attack");
    }

    public void SkillCreate() // Animator Func
    {
        GameObject skill = Instantiate(skillPrefab);
        skill.transform.position = skillPos.position;
    }
    public override void AttackAfter()
    {
        base.AttackAfter();
    }
    public override void DeadAnimScript()
    {
        base.DeadAnimScript();
        GameManager.instance.AddCoin(Random.Range(10, 16));
    }
    public override void GetHitAfter()
    {
        base.GetHitAfter();
    }
}
