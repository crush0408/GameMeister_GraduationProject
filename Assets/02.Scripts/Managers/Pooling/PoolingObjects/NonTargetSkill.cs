using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTargetSkill : PoolableMono
{
    public float damage = 0f;
    public string hitName = "";
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(damage, transform.position,true);
                Debug.Log(hitName);
                
                PoolableMono poolingObject = PoolManager.Instance.Pop(hitName);
                if(poolingObject != null)
                {
                    poolingObject.transform.position = collision.transform.position;
                    
                }
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
