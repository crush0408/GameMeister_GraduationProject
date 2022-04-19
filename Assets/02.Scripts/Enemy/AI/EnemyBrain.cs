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
    

    public GameObject visualGroupObj;
    

    public Transform target;

    public Vector3 rightDirection = Vector3.one;
    public bool isAttacking = false;
    public int attackCount = 1;
    public bool getHit = false;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
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
                visualGroupObj.transform.localScale = rightDirection;
            }
            else
            {
                visualGroupObj.transform.localScale = new Vector3(-rightDirection.x,rightDirection.y,rightDirection.z);
            }
        }
    }
    
    public void GetHit()
    {
        //if(!getHit)
        {
            getHit = true;
            Debug.Log("Animation진입");
            Debug.Log("Animation끝 및 getHit TRUE");
            isAttacking = false;
            FlipSprite();
            anim.SetTrigger("getHit");
        }
    }
    public bool AttackEnd()
    {
        
        return isAttacking;
    }
    public IEnumerator JudgeCoroutine()
    {
        yield return new WaitForSeconds(judgeTime);
        isAttacking = false;
        
    }
    public IEnumerator HitEndCoroutine()
    {
        sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0.5f);
        Debug.Log("Hit적용");
        yield return new WaitForSeconds(1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        getHit = false;
        Debug.Log("Hit콜백");
    }
    public void Dead()
    {
        anim.SetTrigger("isDead");
    }
}
