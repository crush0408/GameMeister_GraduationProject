using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBase : EnemyBase
{
    public virtual void Flying()
    {
        base.Move();

        Vector2 dir = myTarget.transform.position - this.transform.position;
        dir.y = 0f;
        dir.Normalize();

        myVelocity = dir * speed;
        myRigid.velocity = myVelocity;
    }
}
