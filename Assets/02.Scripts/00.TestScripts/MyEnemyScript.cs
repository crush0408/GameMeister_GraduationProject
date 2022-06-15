using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyScript : MYEnemyBase
{
    protected override void CheckTransition()
    {
        switch (myState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Patrolling:
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
        }
    }

    private void Start()
    {
        
    }
}
