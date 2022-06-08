using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public enum EnemyType
    {
        None = -1,
        Walking,
        Flying
    }
    public enum EnemyFsm
    {
        None = -1,
        Idle,
        Chase,
        Patrol,
        Attack,
        GetHit,
        // Boss
        Meditate,
        Pattern,
        SpecialAttack
    }
}
