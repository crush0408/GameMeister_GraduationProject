using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance = null;

    public enum PlayerType
    {
        NONE,
        WATER_POWER,
        LIGHTNING_SPEED,
        ICE_BALANCE
    }

    [SerializeField] protected PlayerType _myType;
    [SerializeField] protected int _hp;
    [SerializeField] protected int _atk;
    [SerializeField] protected int _def;
    [SerializeField] protected int _pass;
    [SerializeField] protected int _moveSpeed;
    [SerializeField] protected int _fragment;   // 재화
    [SerializeField] protected float _atkSpeed;
    [SerializeField] protected float _startTime;

    public PlayerType MyType { get { return _myType; } set { _myType = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int Attack { get { return _atk; } set { _atk = value; } }
    public int Defense { get { return _def; } set { _def = value; } }
    public int Pass { get { return _pass; } set { _pass = value; } }
    public int MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public int Frangment { get { return _fragment; } set { _fragment = value; } }
    public float AttackSpeed { get { return _atkSpeed; } set { _atkSpeed = value; } }
    public float StartTime { get { return _startTime;  } set { _startTime = value; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _myType = PlayerType.NONE;

        _hp = 100;
        _atk = 70;
        _def = 30;
        _pass = 0;
        _moveSpeed = 5;
        _fragment = 0;
        _atkSpeed = 0.7f;

        _startTime = Time.time;
    }
}
