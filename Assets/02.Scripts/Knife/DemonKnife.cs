using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonKnife : MonoBehaviour
{
    //여기서 플레이어 피격 처리?
    //public LayerMask whatIsEnemy;

    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable DamageObj = collision.gameObject.GetComponent<IDamageable>();

            if (DamageObj != null)
            {
                ContactPoint2D contact = collision.contacts[0];

                DamageObj.OnDamage(damage,contact.point);
            }
        }
    }
}
