using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : HpBar
{
    [SerializeField]
    private Text fillTxt;
    
    public void SetHpText(float amount)
    {
        _hp = amount;
        _hp = Mathf.RoundToInt(_hp);
        _maxHp = Mathf.RoundToInt(_maxHp);
        fillTxt.text = string.Format("{0} / {1}",_hp,_maxHp);
    }
    
}
