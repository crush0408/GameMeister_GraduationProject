using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField]
    private AIState _currentState;

    [SerializeField]
    private float _speed = 5f;

    private Rigidbody2D _rigid;

    public Transform target;
    public bool facingRight = true; //현재 오른쪽 보고 있는가?
    private Vector2 destination;

    public Animator ani;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }

    public void ChangeToState(AIState state)
    {
        _currentState = state;
    }
    public void Move(Vector2 dir)
    {
        _rigid.velocity = dir * _speed;

        destination = target.position;

        Vector2 moveDir = destination - (Vector2)transform.position;
        moveDir = moveDir.normalized;
        if (facingRight)
        {
            if (moveDir.x > 0 && transform.localScale.x < 0 || moveDir.x < 0 && transform.localScale.x > 0)
            {
                Flip();
            }
        }
        else
        {
            if (moveDir.x < 0 && transform.localScale.x < 0 || moveDir.x > 0 && transform.localScale.x > 0)
            {
                Flip();
            }
        }
    }
    public void Stop()
    {
        _rigid.velocity = Vector2.zero;
    }
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
