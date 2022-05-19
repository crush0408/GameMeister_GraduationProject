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
        sr = GetComponentInChildren<SpriteRenderer>();
        
        temp = sr.color;
    }

    private void Start()
    {
        healthScript = GetComponentInChildren<PlayerHPBar>();
        healthScript.InitHealth(health, initHealth);
        healthScript.SetHpText(health);

    }

    private IEnumerator ShowDamagedEffect(Vector2 pos,bool push)
    {
        sr.color = Color.red; // 피격 예시
        if (push)
        {
            int reaction = transform.position.x - pos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(reaction * 3, 1), ForceMode2D.Impulse);
        }
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
        // 게임 매니저에서 OnDeath() 액션에 추가
    }

    public override void OnDamage(float damage, Vector2 hitPosition, bool push = true)
    {
        if (isDead) return;
        
        base.OnDamage(damage, hitPosition,push);
        GetComponent<PlayerMove>().GetHitFunc();
        healthScript.SetHpBar(health);
        healthScript.SetHpText(health);
        StartCoroutine(ShowDamagedEffect(hitPosition,push));
        CameraActionScript.ShakeCam(4f, 0.3f, true);
        MGSound.instance.playEff("hitFace");
    }
}
