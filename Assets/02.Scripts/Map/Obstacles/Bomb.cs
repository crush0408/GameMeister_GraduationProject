using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerAttack playerAttack;

    [Header("폭발 반경 및 데미지")]
    public float bombRadius = 2f;
    public float damage = 10f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, bombRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        playerAttack = collision.gameObject.GetComponentInParent<PlayerAttack>();

        if (playerAttack.isAttacking)
        {
            spriteRenderer.color = Color.green; // 확인용(현재 애니메이션이 없어서ㅠㅠ)

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bombRadius);
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

    }
}
