using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    protected Image fill;

    protected float _hp = 0f;
    protected float _maxHp = 0f;

    public void InitHealth(float hp, float maxHp)
    {
        _hp = hp;
        _maxHp = maxHp;
        SetHpBar(_hp);
    }

    public void SetHpBar(float value)
    {
        _hp = value;
        _hp = _hp <= 0 ? 0 : _hp;
        fill.transform.localScale = new Vector3(_hp / _maxHp, 1, 1);
    }
}
