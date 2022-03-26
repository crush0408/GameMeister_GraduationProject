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
    private float _dashTime;
    public float startDashTime = 0.1f;
    public float dashSpeed = 50f;
    public float _dashDis = 1f;
    
    public bool isCrouch = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        _dashTime = startDashTime;
    }
    private void Update()
    {
        ValueSetting();
        GroundCheck();
        
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
        Dash();
        Crouch();
    }
    private void ValueSetting() //Input ó�� ��ȯ
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
            }
            else
            {
                _dashTime -= Time.deltaTime;
                rigid.velocity = new Vector2(_dashDis, 0);
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
                Debug.Log("1�� ����");
                canSecondJump = true;
            }
            else if (!isGround && canSecondJump)
            {
                if (_jump && canSecondJump)
                {
                    _jump = false;
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                    Debug.Log("2�� ����");
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
            // �������� 0.2�� ���� ground�� ������ isGround�� true�� �ٲ��ִ°�
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
