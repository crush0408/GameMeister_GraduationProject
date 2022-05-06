using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Global.EnemyFsm myFsm; 
    public Rigidbody2D myRigid; 
    public Animator myAnim; 
    public Vector3 myVelocity;
    public Vector3 rightDirection;
    public Vector3 leftDirection;
    public GameObject visualGroup;
    public float speed;
    public GameObject myTarget;
    public float sightDistance;
    public float attackDistance;
    public float delayTime;
    public bool isAttacking;
    public bool isDie;
    public EnemyHealth enemyHealth;

    public IEnumerator patrolCoroutine;

    public virtual void Init()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        myTarget = GameManager.instance.playerObj;
        myRigid = GetComponent<Rigidbody2D>();
        myVelocity = Vector3.zero;
        isAttacking = false;
        isDie = false;
        myAnim = GetComponentInChildren<Animator>();
        patrolCoroutine = null;
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
    public virtual IEnumerator Patrol(float random)
    {
        Vector2 dir = new Vector2(random, 0);
        dir.Normalize();
        myVelocity = dir * speed;
        myRigid.velocity = myVelocity;

        if (dir.x > 0)
        {
            visualGroup.transform.localScale = rightDirection;
        }
        else
        {
            visualGroup.transform.localScale = leftDirection;
        }
        yield return new WaitForSeconds(3f);
        patrolCoroutine = null;
    }
    protected void Die()
    {
        isDie = true;
        myAnim.Play("Dead", -1, 0f);
        Debug.Log("Die");
    }
    public virtual void DeadAnimScript()
    {
        Destroy(this.gameObject);
    }
    public virtual void Move()
    {
        FlipSprite();
        myAnim.SetBool("isChase", true);
    }

    
    public virtual void Attack()
    {
        Stop();
        FlipSprite();
        isAttacking = true;
    }
    public virtual void AttackAfter()
    {
        StartCoroutine(AttackDelay());
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(delayTime);
        isAttacking = false;
    }
    protected bool DistanceDecision(float distance)
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
