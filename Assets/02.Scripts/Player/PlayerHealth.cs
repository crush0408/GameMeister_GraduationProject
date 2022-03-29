using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public float damagedEffectTime = 0.1f;
    private Color temp;
    private SpriteRenderer sr;

    private Health healthScript;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        healthScript = GetComponentInChildren<Health>();
        temp = sr.color;
    }

    private void Start()
    {

        healthScript.InitHealth(health, initHealth);
    }

    private IEnumerator ShowDamagedEffect()
    {
        sr.color = Color.red; // �ǰ� ����
        Debug.Log(sr.color);
        yield return new WaitForSeconds(damagedEffectTime);
        sr.color = temp;
    }

    public override void HealHealth(float value)
    {
        base.HealHealth(value);
    }

    public override void Die()
    {
        base.Die();
        // ���� �Ŵ������� OnDeath() �׼ǿ� �߰�
    }

    public override void OnDamage(float damage, Vector2 hitPosition)
    {
        if (isDead) return;
        base.OnDamage(damage, hitPosition);
        healthScript.SetHP(health);
        StartCoroutine(ShowDamagedEffect());
    }
}
