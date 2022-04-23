using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    private float damage = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if(target != null)
            {
                target.OnDamage(damage, transform.position);
                //Debug.Log("적 기본 공격 피격");
            }
        }
    }

}
