using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D rigid;

    public Transform groundCheckTrm;
    public LayerMask whatIsGround;

    public float moveSpeed;
    public float jumpForce;

    public bool isGround;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        
        GroundCheck();
        Move();
        Jump();
    }
    private void FixedUpdate()
    {
    }
    private void Jump()
    {
        if(playerInput.jump && isGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }
        if(playerInput.jumpKeyup && rigid.velocity.y > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
        }
    }
    private void GroundCheck()
    {
        //if (rigid.velocity.y < 0)
        {
            isGround = Physics2D.OverlapCircle(groundCheckTrm.position, 0.2f, whatIsGround); // �������� 0.2�� ���� ground�� ������ isGround�� true�� �ٲ��ִ°�
        }
    }
    private void Move()
    {
        rigid.velocity = new Vector2(playerInput.movement * moveSpeed, rigid.velocity.y);
    }
}
