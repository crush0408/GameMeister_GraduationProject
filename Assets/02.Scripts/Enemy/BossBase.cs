using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBase : EnemyBase, INpc_Monster
{
    public IEnumerator delayCoroutine;
    public bool isDelay = false;
    public float delayTime = 0f;

    public int patternOrder = 0;
    public float jumpPower = 0f;

    public IEnumerator healCoroutine;
    public float healAmount = 0f;
    public bool isMeditating;

    [Header("보스 대화 관련 세팅")]
    public NpcSpeechSystem speechSystem;
    public Data[] datas;
    public bool isTalkEnd = false;
    public bool isTalkStart = false;
    [Space]

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
        isMeditating = false;
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
        
        myAnim.SetTrigger("Jump");
        
    }
    protected virtual IEnumerator HealCoroutine(float amount, float time)
    {
        isMeditating = true;
        yield return new WaitForSeconds(time);

        enemyHealth.HealHealth(amount);

        PoolableMono poolingObject = PoolManager.Instance.Pop("HealEffect");
        poolingObject.transform.parent = this.transform;
        poolingObject.transform.localPosition = Vector3.zero;

        isMeditating = false;
        myAnim.SetBool("isMeditate", isMeditating);
        healCoroutine = null;
    }
    
}
