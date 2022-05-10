using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillCollider : MonoBehaviour
{
    public float damage = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        BoxCollider2D boxCollider = collision.GetComponent<BoxCollider2D>();
        if (collision.gameObject.CompareTag("Player") && collision == boxCollider)
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if(target != null)
            {
                target.OnDamage(damage, transform.position, true);
                Debug.Log(this.gameObject.name + "이 " + damage + "만큼 피해를 입힘");
            }
        }
        */

        // 적용이 안 되어서 주석 처리함

        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if(target != null)
            {
                target.OnDamage(damage, transform.position, true);
                Debug.Log(this.gameObject.name + "이 " + damage + "만큼 피해를 입힘");
            }
        }
    }
}
