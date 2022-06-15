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
    
    protected EnemyState myState = EnemyState.None;
    protected EnemyType myType = EnemyType.None;

    protected void ChangeState(EnemyState state)
    {
        myState = state;
    }
    protected abstract void CheckTransition();
    protected virtual void StartState(EnemyState state)
    {
        ChangeState(state);
        
    }
    //

    // 변수 선언
    protected EnemyHealth myHealth;
    protected Rigidbody2D myRigid;
    protected Animator myAnim;

    protected Vector3 myVelocity = Vector3.zero;
    protected Vector3 lefttDirection = Vector3.zero;
    protected Vector3 rightDirection = Vector3.zero;

    [SerializeField] protected GameObject visualGroup;
    protected GameObject myTarget;

    protected float speed = 0f;
    protected float sightDistance = 0f;
    protected float attackDistance = 0f;

    [HideInInspector] public bool getHit;
    protected bool isAttacking;
    protected bool isDie;
    //

    // 함수
    public virtual void Init()
    {
        myHealth = GetComponent<EnemyHealth>();
        myHealth.OnDead += Die;
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponentInChildren<Animator>();

        myVelocity = Vector3.zero;

        myTarget = GameManager.instance.playerObj;  // 타겟이 Player가 아닐 수도 있지 않을까?

        isAttacking = false;
        isDie = false;
        getHit = false;
    }
    public virtual void Stop() // 움직임을 멈춤
    {
        myVelocity = Vector3.zero;
        myRigid.velocity = myVelocity;
        myAnim.SetBool("isChase",false);
    }
    public virtual void Move()  // 타겟 방향을 향해 이동
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
        // 공격 트리거나 조건 등 처리해야하는것들이 적마다 다르기 때문에, 공격을 할 때 필요한 것만 virtual로 선언
    {
        Stop();
        FlipSprite();
        isAttacking = true;
    }
    public virtual void AttackAfter() // Attack 애니메이션 이후에 isAttacking을 false로 만들어주기 위한 함수
        // 공격 이후 처리가 적마다 필요할 수 있기 때문에 virtual로 선언
    {
        isAttacking = false;
    }
    public virtual void GetHitAfter()
        // 타격 받고 난 후 getHit 처리를 해준다.
    {
        getHit = false;
    }
    private void Die() // Health OnDead액션 함수에 넣음
    {
        isDie = true;
        myAnim.Play("Dead", -1, 0f);    // 죽는 애니메이션 이름 Dead로 통일하기
    }
    public virtual void DestroyFunc() // 죽었을 때 삭제 함수, 죽었을 때 특정 처리를 위해 virtual로 선언
    {
        Destroy(this.gameObject);
    }
    protected void FlipSprite() // target 방향으로 Flip해주는 함수
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
