using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class DemonKnife : MonoBehaviour
{
    //여기서 플레이어 피격 처리?
    //public LayerMask whatIsEnemy;

    public int damage;
    public bool facingRight = false;
    private Vector2 destination;
    private EnemyBrain _enemyBrain = null;

    private void Awake()
    {
        _enemyBrain = GetComponentInParent<EnemyBrain>();
    }

    private void Update()
    {
        destination = _enemyBrain.target.position;
        Vector2 moveDir = destination - (Vector2)transform.position;
        moveDir = moveDir.normalized;
        if (facingRight)
        {
            if (moveDir.x > 0 && transform.localScale.x < 0 || moveDir.x < 0 && transform.localScale.x > 0)
            {
                Flip();
            }
        }
        else
        {
            if (moveDir.x < 0 && transform.localScale.x < 0 || moveDir.x > 0 && transform.localScale.x > 0)
            {
                Flip();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable DamageObj = collision.gameObject.GetComponent<IDamageable>();

            if (DamageObj != null)
            {
                Vector3 contact = collision.transform.position;

                DamageObj.OnDamage(damage, contact);
                CameraActionScript.ShakeCam(4, 0.3f);
            }
        }
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
