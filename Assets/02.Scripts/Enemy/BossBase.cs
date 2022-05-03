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

    public override void Init()
    {
        base.Init();
        speechSystem = GetComponent<NpcSpeechSystem>();
        speechSystem.target = GetComponentInChildren<Text>();
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
        //Jump
        // 현재 위치 에서 플레이어 위치까지 포물선을 그리면서 뛰게 만들기
    }
    protected IEnumerator HealCoroutine(float amount, float time)
    {
        enemyHealth.HealHealth(amount);
        yield return new WaitForSeconds(time);
        healCoroutine = null;
    }
}