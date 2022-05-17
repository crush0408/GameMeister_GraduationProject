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
        Meditate,
        Chase,
        Attack,
        AttackAfter,
        JumpAttackBefore,
        JumpAttack,
        PatternMove,
        GetHit,
        GetHitAfter,
        Delay
    }
}
