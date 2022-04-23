using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float initHealth;
    public float health { get; protected set; }
    public bool isDead { get; protected set; }
    public event Action OnDead;
    protected virtual void OnEnable()
    {
        isDead = false;
        health = initHealth;
    }

    public virtual void OnDamage(float damage, Vector2 hitPosition)
    {
        health -= damage;
        if(health <= 0 && !isDead)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        
        if (OnDead != null) OnDead();

        isDead = true;
    }
    public virtual void HealHealth(float value)
    {
        if (isDead) return;
        health += value;
    }
    
}
