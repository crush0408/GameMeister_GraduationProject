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
        //����� �׳� �����ϸ� �� ��������, �÷��̾ �����ϵ� �¾ҵ� �׷� üũ�� �� ������Դϴ�.
        //if (blood.localScale.x >= 0)
        //{      
        //    blood.localScale = new Vector3(initHealth / 100f, 1, 1); //����ü�� ������ ��ü����
        //}
    }
}
