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

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
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
    }
    public void Stop()
    {
        _rigid.velocity = Vector2.zero;
    }
}
