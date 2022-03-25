using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DemonHPBar : EnemyHealth
{
    private RectTransform blood;

    private void Awake()
    {
        blood = GetComponent<RectTransform>();
    }

    private void Update()
    {
        SetBar();
    }
    public void SetBar()
    {
        //현재는 그냥 실행하면 쭉 내려가고, 플레이어가 공격하든 맞았든 그런 체크를 또 해줘야함니다.
        //if (blood.localScale.x >= 0)
        //{      
        //    blood.localScale = new Vector3(initHealth / 100f, 1, 1); //현재체력 나누기 전체레격
        //}
    }
}
