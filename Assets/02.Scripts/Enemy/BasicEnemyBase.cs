using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBase : EnemyBase
{
    public IEnumerator patrolCoroutine;

    public override void Init()
    {
        base.Init();
        patrolCoroutine = null;
    }

    protected IEnumerator Patrol(float random)
    {
        Vector2 dir = new Vector2(random, 0);
        dir.Normalize();
        myVelocity = dir * speed;
        myRigid.velocity = myVelocity;

        if (dir.x > 0)
        {
            visualGroup.transform.localScale = rightDirection;
        }
        else
        {
            visualGroup.transform.localScale = leftDirection;
        }
        yield return new WaitForSeconds(3f);
        patrolCoroutine = null;
    }
}
