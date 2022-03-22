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
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
