using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Sight
{
    Front = 0,
    Left,
    Right
}

public class EvilCat : EnemyBase
{
    public int seeDirection;

    public bool isSeeFront = false;
    public bool isSeeLeft = false;

    private Vector2 direction;

    public override void Init()
    {
        base.Init();
        sightDistance = 10f;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        direction = myTarget.transform.position - this.transform.position;

        isSeeFront = DistanceDecision(sightDistance) ? true : false;
        isSeeLeft = direction.x < 0 ? true : false;

        myAnim.SetBool("isSeeFront", isSeeFront);
        myAnim.SetBool("isSeeLeft", isSeeLeft);
        myAnim.SetBool("isSeeRight", !isSeeLeft);
    }
}
