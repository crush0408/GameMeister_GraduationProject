using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public float damagedEffectTime = 0.1f;
    private Color temp;
    private SpriteRenderer sr;
    private Rigidbody2D rigid;

    private PlayerHPBar healthScript;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        healthScript = GetComponentInChildren<PlayerHPBar>();
        temp = sr.color;
    }

    private void Start()
    {
        healthScript.InitHealth(health, initHealth);
    }

    private IEnumerator ShowDamagedEffect(Vector2 pos)
    {
        sr.color = Color.red; // �ǰ� ����
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
        int reaction = transform.position.x - hitPosition.x > 0 ? 1 : -1;
        Debug.Log(reaction);
        rigid.AddForce(new Vector2(reaction * 100, 1), ForceMode2D.Impulse);
        StartCoroutine(ShowDamagedEffect(hitPosition));
        base.OnDamage(damage, hitPosition);
        healthScript.SetHP(health);
    }
}
