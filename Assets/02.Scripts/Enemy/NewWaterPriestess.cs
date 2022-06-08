using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWaterPriestess : BossBase
{
    public override void Init()
    {
        base.Init();
        myType = Global.EnemyType.Walking;
        myFsm = Global.EnemyFsm.Idle;

        sightDistance = 12f;
        attackDistance = 5f;
        speed = 3f;             // Idle(Standby) : 3f, Chase : 6f

        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        CheckTransition();  // myFsm 상태 변화만 체크하는 함수(매 프레임마다 호출됨)
    }

    private void CheckTransition()
    {
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:

                break;
            case Global.EnemyFsm.Chase:

                break;
            case Global.EnemyFsm.Attack:

                break;
        }
    }

    private void StartState(Global.EnemyFsm state)
    {
        ChangeState(state); // myFsm 변경
        switch (myFsm)
        {
            case Global.EnemyFsm.Idle:

                break;
            case Global.EnemyFsm.Chase:

                break;
            case Global.EnemyFsm.Attack:

                break;
        }
    }
}
