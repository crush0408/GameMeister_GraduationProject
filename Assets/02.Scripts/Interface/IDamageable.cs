using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void OnDamage(float damage, Vector2 hitPosition);
}
