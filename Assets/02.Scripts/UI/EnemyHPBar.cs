using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : HpBar
{
    
    private void Start()
    {
        // SetInitPosition();
    }

    public void SetInitPosition()
    {
        Collider2D col = GetComponentInParent<Collider2D>();
        float y = (col.bounds.size.y / 2) + 0.6f;
        transform.localPosition = new Vector3(0, y, 0);
    }
}
