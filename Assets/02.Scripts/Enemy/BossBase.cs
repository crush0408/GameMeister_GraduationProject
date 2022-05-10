using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBase : EnemyBase, INpc_Monster
{
    public float patternOrder = 0f;
    public float jumpPower = 0f;

    public IEnumerator healCoroutine;

    public NpcSpeechSystem speechSystem;
    public Data[] datas;

    public bool isTalkEnd = false;
    public bool isTalkStart = false;

    protected Vector2 startPos;
    protected Vector2 lastPos;
    protected Vector2 middlePos;

    public override void Init()
    {
        base.Init();
        //speechSystem = GetComponent<NpcSpeechSystem>();
        //speechSystem.target = GetComponentInChildren<Text>();
        isTalkEnd = false;
        isTalkStart = false;
        healCoroutine = null;
    }
    public void Action(CinemachineVirtualCamera vCam)
    {
        if (!isTalkStart)
        {
            speechSystem.OnStart(datas);
            isTalkStart = true;
            //카메라 세팅?
        }
        else
        {
            if (speechSystem.isEnd(datas))
            {
                isTalkEnd = true;
            }
            else if (speechSystem.isTypingEnd())
            {
                speechSystem.OnNext(datas);
            }
            else
            {
                speechSystem.OnSkip(datas);
            }
        }
    }

    public virtual void Jump()
    {
        isAttacking = true; // 테스팅을 위한 임시 변수 세팅임
        AttackAfter(); // 테스팅을 위한 임시 함수 세팅
        myAnim.SetTrigger("Jump");
        
    }
    protected IEnumerator HealCoroutine(float amount, float time)
    {
        {
            Debug.Log("힐 이전 적 체력 : " + enemyHealth.health);
            enemyHealth.HealHealth(amount);
            Debug.Log("힐 이후 적 체력 : " + enemyHealth.health);
            yield return new WaitForSeconds(time);
        }
        healCoroutine = null;
    }
}
