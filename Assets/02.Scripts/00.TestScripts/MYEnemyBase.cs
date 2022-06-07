using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MYEnemyBase : MonoBehaviour
// EnemyBase는 상속을 해야하는 클래스니까, abstract class로 선언
{
    #region 사용방법
    /* 
     * 밑에서 그냥 사용만 하는 함수는 public or protected로 선언 및 정의
     * 밑에서 무조건 사용해야하는 함수라면 abstract, 필요에 따라 사용하고 싶다면 virtual
    public void FirstFunc() {}
    public abstract void SecondFunc();
    public virtual void ThirdFunc() {}
    */
    #endregion
    // EnemyState, Type
    protected enum EnemyState
    {
        None = -1,
        Idle,
        Patrolling,
        Chase,
        Attack
    }
    protected enum EnemyType
    {
        None = -1,
        NonFlightable,
        Flightable
    }
    [Header("Enemy Type, State")]
    [SerializeField] protected EnemyState myState = EnemyState.None;
    [SerializeField] protected EnemyType myType = EnemyType.None;

    protected void ChangeState(EnemyState state)
    {
        myState = state;
    }
    //

    // 변수 선언
    [Header("변수")]
    [SerializeField] protected EnemyHealth myHealth;
    [SerializeField] protected Rigidbody2D myRigid;
    [SerializeField] protected Animator myAnim;
    [Space]
    [SerializeField] protected Vector3 myVelocity = Vector3.zero;
    [SerializeField] protected Vector3 lefttDirection = Vector3.zero;
    [SerializeField] protected Vector3 rightDirection = Vector3.zero;
    [Space]
    [SerializeField] protected GameObject visualGroup;
    [SerializeField] protected GameObject myTarget;
    [Space]
    [SerializeField] protected float speed = 0f;
    [SerializeField] protected float sightDistance = 0f;
    [SerializeField] protected float attackDistance = 0f;
    [Space]
    public bool getHit;
    [SerializeField] protected bool isAttacking;
    [SerializeField] protected bool isDie;
    //

    // 함수
    public virtual void Init()
    {
        myHealth = GetComponent<EnemyHealth>();
        myHealth.OnDead += Die;
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponentInChildren<Animator>();

        myVelocity = Vector3.zero;

        myTarget = GameManager.instance.playerObj;

        isAttacking = false;
        isDie = false;
        getHit = false;
    }
    public virtual void Stop()
    {
        myVelocity = Vector3.zero;
        myRigid.velocity = myVelocity;
        myAnim.SetBool("isChase",false);
    }
    public virtual void Move()
    {
        FlipSprite();
        myAnim.SetBool("isChase", true);

        Vector2 dir = myTarget.transform.position - this.transform.position;
        if(myType == EnemyType.Flightable) { dir.y = 0f; }
        dir.Normalize();

        myVelocity = dir * speed;
        myRigid.velocity = myVelocity;
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
    private void Die()
    {
        isDie = true;
        myAnim.Play("Dead", -1, 0f);    // 죽는 애니메이션 이름 Dead로 통일하기
    }
    public virtual void DestroyFunc()
    {
        Destroy(this.gameObject);
    }
    protected void FlipSprite()
    {
        Vector2 dir = myTarget.transform.position - this.transform.position;
        visualGroup.transform.localScale = dir.x > 0 ? rightDirection : lefttDirection;
    }
    protected bool DistanceDecision(float distance)
        // 플레이어와 적의 거리가 distance보다 작은지 bool 리턴
    {
        float calc = (myTarget.transform.position - this.transform.position).sqrMagnitude;
        return calc < distance * distance;
    }
    //


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