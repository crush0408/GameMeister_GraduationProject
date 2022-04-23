using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillCollider : MonoBehaviour
{
    public float damage = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"Trigger { collision.gameObject.tag}");
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player");
            IDamageable target = collision.GetComponent<IDamageable>();
            if(target != null)
            {
                //Debug.Log("피격");
                target.OnDamage(damage, this.gameObject.transform.position);
            }
        }
    }
}
