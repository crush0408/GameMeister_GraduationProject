using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public enum EnemyFsm
    {
        None = -1,
        Idle,
        Patrol,
        Heal,
        Chase,
        Attack,
        AttackAfter,
        Delay
    }
}
