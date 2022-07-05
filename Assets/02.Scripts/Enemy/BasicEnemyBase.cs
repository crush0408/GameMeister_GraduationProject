using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBase : EnemyBase
{
    public IEnumerator patrolCoroutine;
    public float patrolCoolTime;
    public bool isPatroling;
    public float patDir;

    public override void Init()
    {
        base.Init();
        patrolCoroutine = null;
    }
    
    protected IEnumerator Patrol()
    {
        //Debug.Log("Patrol Start");
        
        isPatroling = true;
        patDir = Random.Range(-1, 2);
        
        myVelocity = new Vector2(patDir * speed, myVelocity.y);
        myRigid.velocity = myVelocity;
        if (patDir != 0)
        {
            myAnim.SetBool("isChase", true);
            PatrolFlip(patDir);
        }
        yield return new WaitForSeconds(patrolCoolTime);
        isPatroling = false;
        patrolCoroutine = null;
    }

    protected void PatrolFlip(float dir)
    {
        if(dir > 0)
        {
            visualGroup.transform.localScale = rightDirection;
        }
        else
        {
            visualGroup.transform.localScale = leftDirection;
        }
    }
}
