using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D rigid;
    private Animator anim;

    public Transform groundCheckTrm;
    public LayerMask whatIsGround;

    public float moveSpeed;
    public float jumpForce;

    public bool isGround;
    public bool canSecondJump = false;
    public bool canDash = true;

    private bool _jump;
    private bool _jumpKeyUp;
    private bool _dash;
    private float dashDir = 0;
    
    public bool isCrouch = false;

    
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(playerInput.movement != 0)
        {
            dashDir = playerInput.movement;
        }
        if(playerInput.jump)
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
        GroundCheck();
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
        Dash();
        //Crouch();
    }
    private void Dash()
    {
        if(_dash && canDash)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(new Vector2(dashDir * 2f, 0), ForceMode2D.Impulse);
        }
    }
    private void Crouch()
    {
        if (playerInput.crouch)
        {
            isCrouch = true;
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
                Debug.Log("1단 점프");
                canSecondJump = true;
            }
            else if (canSecondJump)
            {
                if (_jump && canSecondJump)
                {
                    _jump = false;
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                    Debug.Log("2단 점프");
                    canSecondJump = false;
                }
            }
        }
        if(_jumpKeyUp && rigid.velocity.y > 0)
        {
            _jumpKeyUp = false;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
        }
        /*
        if(playerInput.jump && isGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            Debug.Log("1단 점프");
            canSecondJump = true;
        }
        */
        /*
        if(playerInput.jump && canSecondJump)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            Debug.Log("2단 점프");
            canSecondJump = false; 
        }
        */

    }
    private void GroundCheck()
    {
        //if (rigid.velocity.y < 0)
        {
            isGround = Physics2D.OverlapCircle(groundCheckTrm.position, 0.2f, whatIsGround); // 반지름이 0.2인 원에 ground가 닿으면 isGround를 true로 바꿔주는것
            if (isGround)
            {
                canDash = true;
            }
        }
    }
    private void Move()
    {
        rigid.velocity = new Vector2(playerInput.movement * moveSpeed, rigid.velocity.y);
    }
}
