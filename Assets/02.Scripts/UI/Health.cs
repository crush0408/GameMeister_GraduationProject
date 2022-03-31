using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;
    private SpriteRenderer fillSpriteRenderer;

    private float _hp, _maxHp;

    private void Start()
    {
        healthBar = this.gameObject;
        fillSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        SetBarX(Mathf.Lerp(healthBar.transform.localScale.x, _hp / _maxHp, Time.deltaTime));
        fillSpriteRenderer.color = Color.Lerp(Color.red, Color.green, (_hp / _maxHp));
    }

    private void SetBarX(float x)
    {
        healthBar.transform.localScale = new Vector3(x, healthBar.transform.position.y, healthBar.transform.position.z);
    }

    public void InitHealth(float hp, float maxHp)
    {
        _hp = hp;
        _maxHp = maxHp;
    }

    public void SetHP(float hp)
    {
        _hp = hp;
    }
}
