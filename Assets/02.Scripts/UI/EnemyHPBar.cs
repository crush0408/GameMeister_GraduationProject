using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : HpBar
{

    public void SetInitPosition()
    {
        Collider2D col = GetComponentInParent<Collider2D>();
        float y = -(col.bounds.size.y / 2) + 0.2f;
        transform.localPosition = new Vector3(0, y, 0);
    }
}
