using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimFunc : MonoBehaviour
{
    EnemyBrain enemyBrain;
    private void Awake()
    {
        enemyBrain = GetComponentInParent<EnemyBrain>();
    }
    public void DestroyObjectFunc()
    {
        Destroy(enemyBrain.gameObject);
    }
    public void AttackEndAnimFunc()
    {
        StartCoroutine(enemyBrain.JudgeCoroutine());
    }
    public void GetHitEndAnimFunc()
    {
        enemyBrain.getHit = false;
    }
}
