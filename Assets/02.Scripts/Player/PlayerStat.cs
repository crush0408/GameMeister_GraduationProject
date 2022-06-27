using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] protected int _hp;
    [SerializeField] protected int _gold;
    [SerializeField] protected int _atk;
    [SerializeField] protected int _def;
    [SerializeField] protected int _pass;
    [SerializeField] protected int _atkSpeed;
    [SerializeField] protected int _moveSpeed;

    public int HP { get { return _hp; } set { _hp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public int Attack { get { return _atk; } set { _atk = value; } }
    public int Defense { get { return _def; } set { _def = value; } }
    public int Pass { get { return _pass; } set { _pass = value; } }
    public int AttackSpeed { get { return _atkSpeed; } set { _atkSpeed = value; } }
    public int MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
        _hp = 100;
        _gold = 0;
        _atk = 70;
        _def = 30;
        _pass = 0;
        _atkSpeed = 0.7;
        _moveSpeed = 5;
    }
}
