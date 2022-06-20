using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thronbush : MonoBehaviour
{
    public float damage = 0f;
    public bool push = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BoxCollider2D boxCollider = collision.GetComponent<BoxCollider2D>();

        if (collision.gameObject.CompareTag("Player") && boxCollider == collision)
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(damage, transform.position, push);
            }
        }
    }
}
