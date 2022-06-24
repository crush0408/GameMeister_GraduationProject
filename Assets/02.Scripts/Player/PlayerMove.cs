using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private RePlayerInput playerInput;
    private PlayerAttack playerAttack;
    private Rigidbody2D rigid;
    private Animator anim;

    public Transform groundCheckTrm;
    public LayerMask whatIsGround;

    public float moveSpeed;
    public float jumpForce;

    public bool isGround;
    public bool canSecondJump = false;
    public bool canDash = true;
    public bool canMove = true;

    public bool getHit = false;

    public Transform leftLimit;
    public Transform rightLimit;

    private bool _jump;
    private bool _jumpKeyUp;
    private bool _dash;

    [Header("Dash 관련 변수들")]
    [SerializeField] private float _dashDelayStartTime;
    private float _dashDelayTime;
    private float _dashTime;
    [SerializeField] private float startDashTime = 0.1f;
    [SerializeField] private float dashSpeed = 50f;
    [SerializeField] private float _dashDis = 1f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<RePlayerInput>();
        playerAttack = GetComponent<PlayerAttack>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        _dashTime = startDashTime;
        _dashDelayTime = 0f;
        _dashDis = dashSpeed * 1;
        _dashDelayTime = 1f;
    }
    private void Update()
    {
        ValueSetting();
        GroundCheck();
        Col();
    }
    public void Col()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider2d in colliders)
        {
            IDoor door = collider2d.GetComponent<IDoor>();
            if (door != null && Input.GetKeyDown(KeyCode.E))
            {
                door.Action();
            }
        }
    }
    private void FixedUpdate()
    {
        //if (!playerAttack.isAttacking)
        {
            Move();
            Jump();
            Dash();
        }
    }
    private void ValueSetting() //Input 처리 변환
    {
        if (playerInput.movementLeft) 
            _dashDis = dashSpeed * -1;
        else if (playerInput.movementRight) 
            _dashDis = dashSpeed;

        if (!playerAttack.isAttacking)
        {

            if (playerInput.jump)
            {
                _jump = true;
            }
            if (playerInput.jumpKeyUp)
            {
                _jumpKeyUp = true;
            }

            if (playerInput.dash && _dashDelayTime <= 0)
            {
                _dash = true;
            }
        }
    }
    private void Dash()
    {
        if (_dash && canDash && _dashDelayTime <= 0)
        {
            if (_dashTime <= 0)
            {
                _dash = false;
                canDash = false;
                _dashDelayTime = _dashDelayStartTime;
                _dashTime = startDashTime;
                rigid.velocity = Vector2.zero;
                anim.SetBool("isDash", false);
            }
            else
            {
                _dashTime -= Time.deltaTime;
                rigid.velocity = new Vector2(_dashDis, 0);
                anim.SetBool("isDash", true);
                MGSound.instance.playEff("PlayerDash");
            }
        }
        else if (_dashDelayTime > 0)
        {
            _dashDelayTime -= Time.deltaTime;
            if (isGround)
            {
                canDash = true;
            }
        }
        else if (!canDash)
        {
            if (isGround)
            {
                canDash = true;
            }
        }
    }

    private void Jump()
    {
        if (_jump)
        {
            if (isGround)
            {
                _jump = false;
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                anim.SetTrigger("isJump");
                //Debug.Log("1단 점프");
                canSecondJump = true;
            }
            else if (!isGround && canSecondJump)
            {
                _jump = false;
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                anim.SetTrigger("isJump");
                //Debug.Log("2단 점프");
                canSecondJump = false;
            }
            MGSound.instance.playEff("PlayerJump");
        }
        else if (!isGround && rigid.velocity.y < 0)
        {

        }


        /*if(_jumpKeyUp && rigid.velocity.y < 0)
        {
            _jumpKeyUp = false;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 2f);
        }*/

    }
    private void GroundCheck()
    {
        //if (rigid.velocity.y < 0)
        {
            isGround = Physics2D.OverlapCircle(groundCheckTrm.position, 0.1f, whatIsGround);
            // 반지름이 0.2인 원에 ground가 닿으면 isGround를 true로 바꿔주는것
            anim.SetBool("isGround", isGround);
            if (isGround)
            {
                //canDash = true;
                canSecondJump = false;

                playerAttack.canJumpAttack = false;
            }
            else
            {
                playerAttack.canJumpAttack = true;
            }
        }
    }
    private void Move()
    {
        if (!playerAttack.isAttacking)
        {
            if (!getHit)
            {
                if (playerInput.movementLeft && !playerInput.movementRight)
                {
                    rigid.velocity = new Vector2(-1 * moveSpeed, rigid.velocity.y);
                }
                else if(playerInput.movementRight && !playerInput.movementLeft)
                {
                    rigid.velocity = new Vector2(1 * moveSpeed, rigid.velocity.y);
                }
                else
                {
                    rigid.velocity = new Vector2(0 * moveSpeed, rigid.velocity.y);
                }
            }
            anim.SetBool("movement", (playerInput.movementRight || playerInput.movementLeft));
            anim.SetFloat("ySpeed", rigid.velocity.y);
            if (playerInput.movementRight)
            {
                playerAttack.visualGroup.transform.localScale = Vector3.one;
                //MGSound.instance.playEff("PlayerMove");
            }
            else if (playerInput.movementLeft)
            {
                playerAttack.visualGroup.transform.localScale = new Vector3(-1, 1, 1);
                //MGSound.instance.playEff("PlayerMove");
            }
        }
    }
    public void GetHitFunc()
    {
        getHit = true;
        GetComponent<PlayerAttack>().isAttacking = false;
        anim.SetTrigger("GetHit");
    }
    public void GetHitReturn()
    {
        getHit = false;
    }
}
