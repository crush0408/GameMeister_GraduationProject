using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    public float damagedEffectTime = 0.1f;
    private SpriteRenderer sr;
    private Rigidbody2D rigid;
    private Color temp;

    public GameObject hpBarPrefab;
    public EnemyHPBar hpBar;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        temp = sr.color;
    }
    private void Start()
    {
        GameObject hpbar = Instantiate(hpBarPrefab, this.transform);
        hpBar = hpbar.GetComponent<EnemyHPBar>();
        hpBar.SetInitPosition();
        hpBar.InitHealth(health, initHealth);
    }
    
    
    private IEnumerator ShowDamagedEffect(Vector2 pos, bool push)
    {

        sr.color = Color.red; // 피격 예시
        if (push)
        {
            int reaction = transform.position.x - pos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(reaction * 5, 1), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(damagedEffectTime);
        sr.color = temp;
    }
    public override void HealHealth(float value)
    {
        base.HealHealth(value);
        hpBar.SetHpBar(health);
    }
    public override void Die()
    {
        base.Die();
        //오브젝트 없애는 게 필요할듯 setactive나 destroy
        // * GroundBoss 에서 Dead 애니메이션에 붙어있는 이벤트 함수 DeadAfter()에서 SetActive 처리함
        //DeadCoroutine으로 효과까지
        MGSound.instance.playEff("blooding");
        Debug.Log("Dead");
    }
    public override void OnDamage(float damage, Vector2 hitPosition, bool push)
    {
        if (isDead) return;

        GetComponent<EnemyBase>().getHit = true;
        
        base.OnDamage(damage, hitPosition,push);
        hpBar.SetHpBar(health);

        PoolableMono a = PoolManager.Instance.Pop("DamageText");
        a.transform.position = this.transform.position;
        a.GetComponent<DamageText>().PlayFloating(damage.ToString());
        
        StartCoroutine(ShowDamagedEffect(hitPosition, push));
        CameraActionScript.ShakeCam(2f, 0.2f,false);
        MGSound.instance.playEff("hitFace");
    }
    
}
