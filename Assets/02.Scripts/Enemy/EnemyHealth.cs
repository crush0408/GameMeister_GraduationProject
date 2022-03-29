using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    public float damagedEffectTime = 0.1f;
    private SpriteRenderer sr;
    private Color temp;
    DemonAni demonAni;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        temp = sr.color;
        demonAni = GetComponent<DemonAni>();
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Test1");
            StartCoroutine(ShowDamagedEffect());
        }
        */
    }
    private IEnumerator ShowDamagedEffect()
    {
        
        sr.color = Color.red; // 피격 예시
        Debug.Log(sr.color);
        yield return new WaitForSeconds(damagedEffectTime);
        sr.color = temp;
        Debug.Log(health);
    }
    public override void HealHealth(float value)
    {
        base.HealHealth(value);
    }
    public override void Die()
    {
        base.Die();
        demonAni.setDead();
        //오브젝트 없애는 게 필요할듯 setactive나 destroy
    }
    public override void OnDamage(float damage, Vector2 hitPosition)
    {
        if (isDead) return;
        base.OnDamage(damage, hitPosition);
        StartCoroutine(ShowDamagedEffect());
        demonAni.setHit();
    }
}
