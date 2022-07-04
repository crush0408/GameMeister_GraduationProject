using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    private float damage = 5f;  // 플레이어 데미지
    [SerializeField]
    private bool push = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if(target != null)
            {
                float denominator = 100 + PlayerStat.instance.Defense - PlayerStat.instance.Pass;
                float afterDamage =
                    (100 / denominator * (damage + PlayerStat.instance.Attack));
                target.OnDamage(afterDamage, transform.position,push);
                Debug.Log("뎀미징 : " + afterDamage);
                //Debug.Log("적 기본 공격 피격");
            }
        }
    }

}
