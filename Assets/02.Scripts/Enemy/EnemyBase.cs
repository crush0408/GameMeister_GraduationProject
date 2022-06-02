using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Global.EnemyFsm myFsm;
    public Global.EnemyType myType;
    public Rigidbody2D myRigid; 
    public Animator myAnim; 

    public Vector3 myVelocity;
    public Vector3 rightDirection;
    public Vector3 leftDirection;

    public GameObject visualGroup;
    public GameObject myTarget;
    
    public float speed;
    public float sightDistance;
    public float attackDistance;
    
    public bool getHit;
    public bool isAttacking;
    public bool isDie;

    public EnemyHealth enemyHealth;

    public virtual void Init()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        myTarget = GameManager.instance.playerObj;
        myRigid = GetComponent<Rigidbody2D>();
        myVelocity = Vector3.zero;
        isAttacking = false;
        isDie = false;
        myAnim = GetComponentInChildren<Animator>();
        enemyHealth.OnDead += Die;
    }

    protected void ChangeState(Global.EnemyFsm state)
    {
        myFsm = state;
    }
    
    public virtual void Stop()
    {
        myVelocity = Vector2.zero;
        myRigid.velocity = myVelocity;
        myAnim.SetBool("isChase", false);
    }
    
    protected void Die()
    {
        isDie = true;
        myAnim.Play("Dead", -1, 0f);    // 죽는 애니메이션 이름 Dead로 통일하기
        Debug.Log("Die : " + this.gameObject.name);
        
    }

    public virtual void DeadAnimScript()
    {
        Destroy(this.gameObject);
    }

    public virtual void Chase()
    {
        FlipSprite();
        myAnim.SetBool("isChase", true);

        Vector2 dir = myTarget.transform.position - this.transform.position;
        if(myType == Global.EnemyType.Walking)
        {
            dir.y = 0f;
        }
        dir.Normalize();

        myVelocity = dir * speed;
        myRigid.velocity = myVelocity;
    }

    protected void GetHit()
    {
        FlipSprite();
        if(!enemyHealth.isDead)
        {
            myAnim.Play("Hit");
            if (isAttacking) isAttacking = false;
        }
    }

    public virtual void GetHitAfter()
    {
        getHit = false;
    }

    public virtual void Attack()
    {
        Stop();
        FlipSprite();
        isAttacking = true;
        
    }

    public virtual void AttackAfter()
    {
        isAttacking = false;
    }
    
    protected bool DistanceDecision(float distance) // 플레이어와 적의 거리가 distance보다 작은지 bool 리턴
    {
        float calc = Vector3.Distance(myTarget.transform.position, transform.position);

        return calc < distance;
    }

    protected void FlipSprite()
    {
        Vector2 dir = myTarget.transform.position - this.transform.position;
        if(dir.x > 0)
        {
            visualGroup.transform.localScale = rightDirection;
        }
        else
        {
            visualGroup.transform.localScale = leftDirection;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, sightDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
#endif
}
