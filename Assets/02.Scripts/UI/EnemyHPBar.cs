using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField]
    private Transform healthBar;
    private SpriteRenderer fillSpriteRenderer;

    private float _hp = 0f;
    private float _maxHp = 0f;

    private void Start()
    {
        healthBar = gameObject.transform;
        fillSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        transform.localScale = new Vector3(Mathf.Round(_hp / _maxHp), 0f, 0f);
    }

    public void InitHealth(float hp, float maxHp)
    {
        _hp = hp;
        _maxHp = maxHp;

        Debug.Log("hp : " + hp + " maxHp : " + maxHp + " | 여기 hp : " + _hp + " 여기 maxHp : " + _maxHp);
    }

    public void SetHP(float hp)
    {
        _hp = hp > 0 ? hp : 0;
    }
}
