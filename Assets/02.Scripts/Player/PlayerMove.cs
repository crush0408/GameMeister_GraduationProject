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
    public bool canMove = true;


    private float _movement = 0f;
    private bool _jump;
    private bool _jumpKeyUp;
    private bool _dash;
    public Vector2 _dashTarget = Vector2.zero;
    public float _dashDis = 3f;
    
    public bool isCrouch = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        ValueSetting();
        GroundCheck();
        _dashTarget = new Vector2(transform.position.x + _dashDis, transform.position.y);
        
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
        Dash();
        Crouch();
    }
    private void ValueSetting() //Input 처리 변환
    {
        if (playerInput.movement != 0)
        {
            _dashDis = 3 * playerInput.movement;
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
            StartCoroutine(DashCoroutine());
        }
    }
    IEnumerator DashCoroutine()
    {
        _dash = false;
        canDash = false;
        Debug.Log("Dash Start");
        float a = 0;
        while (a < 1f)
        {
            transform.position = Vector2.Lerp(transform.position, _dashTarget, a);
            a += 0.001f;
        }
        Debug.Log("Dash End");
        canDash = true;
        yield return null;
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
                Debug.Log("1단 점프");
                canSecondJump = true;
            }
            else if (!isGround && canSecondJump)
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
            isGround = Physics2D.OverlapCircle(groundCheckTrm.position, 0.2f, whatIsGround); 
            // 반지름이 0.2인 원에 ground가 닿으면 isGround를 true로 바꿔주는것
            if (isGround)
            {
                canDash = true;
            }
        }
    }
    private void Move()
    {
        if (!isCrouch)
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
