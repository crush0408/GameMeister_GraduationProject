using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : HpBar
{
    [SerializeField]
    private Text fillTxt;
    
    public void SetHpText(float _hp, float _maxHp)
    {
        fillTxt.text = string.Format("{0} / {1}",_hp,_maxHp);
    }
    
}
