using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillCollider : MonoBehaviour
{
    public float damage = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BoxCollider2D boxCollider = collision.GetComponent<BoxCollider2D>();

        Debug.Log("EnemySkillCollider 작동 확인용"); // 작동 확인됨
        Debug.Log(collision.gameObject.name);   // 계속 TestMap만 찍히는 상태

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("진입 확인용");    // if문 안으로 진입하지 못하고 있음

            IDamageable target = collision.GetComponent<IDamageable>();
            if(target != null)
            {
                target.OnDamage(damage, transform.position, true);
                Debug.Log(this.gameObject.name + "이 " + damage + "만큼 피해를 입힘");
            }
        }
    }
}
