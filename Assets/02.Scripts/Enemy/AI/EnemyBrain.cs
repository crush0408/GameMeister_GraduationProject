using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    private EnemyHealth enemyHealth;
    

    public GameObject visualGroupObj;


    

    public Transform target;

    public Vector3 rightDirection = Vector3.one;
    private Vector3 leftDirection = Vector3.zero;
    public bool isAttacking = false;
    public int attackCount = 1;
    public bool getHit = false;

    public bool isRecognizePlayer = false;
    private bool isDead = false;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        target = GameManager.instance.playerObj.transform;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
    }

    private void Update()
    {
        if (!isDead)
        {
            _currentState?.UpdateState();
        }
    }

    public void ChangeToState(AIState state)
    {
        _currentState = state;
    }
    public void Move(Vector2 dir)
    {
        if (!isRecognizePlayer)
        {
            isRecognizePlayer = true;
        }

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
                visualGroupObj.transform.localScale = leftDirection;
            }
        }
    }
    
    public void GetHit()
    {
        FlipSprite();
        if(!enemyHealth.isDead)
        {

            //anim.SetTrigger("getHit");
            anim.Play("Hit", -1, 0f);
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
        //sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0.5f);
        
        yield return new WaitForSeconds(1f);
        //sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        getHit = false;
    }
    public void Dead()
    {
        isDead = true;
        anim.Play("Dead",-1,0f);
        //anim.SetTrigger("isDead");
        Debug.Log("DeadAnim");
    }
}
