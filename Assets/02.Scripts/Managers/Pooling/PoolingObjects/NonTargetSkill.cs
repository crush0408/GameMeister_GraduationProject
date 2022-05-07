using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTargetSkill : PoolableMono
{
    public float damage = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(damage, transform.position);
            }
        }
    }

    public void PushFunc()
    {
        PoolManager.Instance.Push(this);
        
    }

    public override void Reset()
    {
        
    }
}
