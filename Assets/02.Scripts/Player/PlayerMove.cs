using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
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


    private float _movement = 0f;
    private bool _jump;
    private bool _jumpKeyUp;
    private bool _dash;
    private float _dashTime;
    public float startDashTime = 0.1f;
    public float dashSpeed = 50f;
    public float _dashDis = 1f;
    
    public bool isCrouch = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAttack = GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        _dashTime = startDashTime;
        _dashDis = dashSpeed * 1;
    }
    private void Update()
    {
        ValueSetting();
        GroundCheck();
        
    }
    private void FixedUpdate()
    {
        //if (!playerAttack.isAttacking)
        {

            Move();
            Jump();
            Dash();
            Crouch();
        }
    }
    private void ValueSetting() //Input 처리 변환
    {
        if (playerInput.movement != 0)
        {
            _dashDis = dashSpeed * playerInput.movement;
        }
        if (playerInput.jump)
        {
            _jump = true;
        }

        if (playerInput.jumpKeyup)
        {
            _jumpKeyUp = true;
        }
        if (playerInput.dash)
        {
            _dash = true;
        }

        if (playerInput.crouch)
        {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
        }
        
    }
    private void Dash()
    {
        if(_dash && canDash)
        {
            if(_dashTime <= 0)
            {
                _dash = false;
                canDash = false;
                _dashTime = startDashTime;
                rigid.velocity = Vector2.zero;
                anim.SetBool("isDash", false);
            }
            else
            {
                _dashTime -= Time.deltaTime;
                rigid.velocity = new Vector2(_dashDis, 0);
                anim.SetBool("isDash",true);
            }

        }
    }
    
    private void Crouch()
    {
        if (isCrouch)
        {
            anim.SetBool("isCrouch", true);   
        }
        else
        {
            anim.SetBool("isCrouch", false);
            isCrouch = false;
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
                Debug.Log("1단 점프");
                canSecondJump = true;
            }
            else if (!isGround && canSecondJump)
            {
                if (_jump && canSecondJump)
                {
                    _jump = false;
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                    anim.SetTrigger("isJump");
                    Debug.Log("2단 점프");
                    canSecondJump = false;
                }
            }
        }
        else if(!isGround && rigid.velocity.y < 0)
        {
            anim.SetTrigger("isDown");
        }
        
        /*
        if(_jumpKeyUp && rigid.velocity.y > 0)
        {
            _jumpKeyUp = false;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.7f);
        }
        */
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
                canDash = true;
            }
        }
    }
    private void Move()
    {
        if (!isCrouch && !playerAttack.isAttacking)
        {
            rigid.velocity = new Vector2(playerInput.movement * moveSpeed, rigid.velocity.y);
            anim.SetBool("movement", playerInput.movement != 0);
        
            if(playerInput.movement == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if(playerInput.movement == -1)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true; 
            }
        }
    }
}
