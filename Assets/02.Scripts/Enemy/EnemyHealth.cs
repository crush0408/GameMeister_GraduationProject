using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    public float damagedEffectTime = 0.1f;
    private SpriteRenderer sr;
    private Rigidbody2D rigid;
    private EnemyBrain _enemyBrain;
    private Color temp;

    // public EnemyHPBar healthScript;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        _enemyBrain = GetComponent<EnemyBrain>();
        temp = sr.color;
    }
    private void Start()
    {
        // healthScript.InitHealth(health, initHealth);
    }
    private void Update()
    {
        
        
        
    }
    private IEnumerator ShowDamagedEffect(Vector2 pos)
    {
        
        sr.color = Color.red; // 피격 예시
        Debug.Log(sr.color);
        int reaction = transform.position.x - pos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(reaction * 5, 1), ForceMode2D.Impulse);
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
        //오브젝트 없애는 게 필요할듯 setactive나 destroy
        //DeadCoroutine으로 효과까지
        _enemyBrain.Dead();
    }
    public override void OnDamage(float damage, Vector2 hitPosition)
    {
        if (isDead) return;
        base.OnDamage(damage, hitPosition);
        _enemyBrain.getHit = true;
        _enemyBrain.isAttacking = false;
        CameraActionScript.ShakeCam(2f, 0.2f,false);
        StartCoroutine(ShowDamagedEffect(hitPosition));
        //healthScript.SetHP(health);

    }
    
}
