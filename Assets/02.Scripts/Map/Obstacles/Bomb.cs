using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerAttack playerAttack;

    [Header("폭발 반경 및 데미지")]
    public float range = 2f;
    public float damage = 10f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerAttackCol")) return;

        playerAttack = collision.gameObject.GetComponentInParent<PlayerAttack>();

        if (playerAttack.isAttacking)
        {
            spriteRenderer.color = Color.green; // 확인용(현재 애니메이션이 없어서ㅠㅠ)

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("에너미 폭발");
                    col.gameObject.GetComponent<EnemyHealth>().OnDamage(damage, transform.position, true);
                }
            }
        }
    }

    public void AfterExplode()  // 폭발 애니메이션 이후 이벤트 함수
    {
        // 없애거나 끄는 코드 넣기(일회성 폭탄)
    }
}
