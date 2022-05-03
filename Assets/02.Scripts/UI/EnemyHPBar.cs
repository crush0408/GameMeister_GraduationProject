using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Image fill;

    private float _hp = 0f;
    private float _maxHp = 0f;

    private void Start()
    {
        SetInitPosition();
    }

    public void SetInitPosition()
    {
        Collider2D col = GetComponentInParent<Collider2D>();
        Debug.Log(col.bounds.size.y);
        Debug.Log(col.bounds.size.y / 2);
        float y = (col.bounds.size.y / 2) + 0.6f;
        Debug.Log(y);
        transform.localPosition = new Vector3(0, y, 0);
    }

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
        fill.transform.localScale = new Vector3(_hp/_maxHp,1,1);
    }
}
