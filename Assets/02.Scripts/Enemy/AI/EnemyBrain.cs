using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField]
    private AIState _currentState;

    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float judgeTime = 3f;

    private Rigidbody2D _rigid;
    private Animator anim;
    private SpriteRenderer sr;
    private EnemyHealth enemyHealth;
    

    public Transform target;

    public bool rightDirection;
    public bool isAttacking = false;
    public int attackCount = 1;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        target = GameManager.instance.playerObj.transform;
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }

    public void ChangeToState(AIState state)
    {
        _currentState = state;
    }
    public void Move(Vector2 dir)
    {
        FlipSprite();

        _rigid.velocity = dir * _speed;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
        anim.SetBool("isChase", true);

    }
    public void Stop()
    {
        _rigid.velocity = Vector2.zero;
        anim.SetBool("isChase", false);
    }
    public void Attack()
    {
        if(enemyHealth.health / enemyHealth.initHealth > 0.6f)
        {
            if(attackCount % 2 == 0)
            {
                // 짝수
                anim.SetTrigger("isAttackTwo");
            }
            else
            {
                // 홀수
                anim.SetTrigger("isAttack");

            }
        }
        else
        {
            anim.SetTrigger("isAttackThree");
        }
        attackCount++;
    }
    public void FlipSprite()
    {
        if (!isAttacking)
        {
            Vector2 dir = target.position - transform.position;
            dir.Normalize();
            if(dir.x > 0)
            {
                sr.flipX = rightDirection;
            }
            else
            {
                sr.flipX = !rightDirection;
            }
        }
    }
    public void AttackEndAnimFunc()
    {
        StartCoroutine(JudgeCoroutine());
    }
    public bool AttackEnd()
    {
        
        return isAttacking;
    }
    private IEnumerator JudgeCoroutine()
    {
        yield return new WaitForSeconds(judgeTime);
        isAttacking = false;
    }
}
