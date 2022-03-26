using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonKnife : MonoBehaviour
{
    //여기서 플레이어 피격 처리?
    //public LayerMask whatIsEnemy;

    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable DamageObj = collision.gameObject.GetComponent<IDamageable>();

            if (DamageObj != null)
            {
                Vector3 contact = collision.transform.position;

                DamageObj.OnDamage(damage, contact);
            }
        }
    }
}
