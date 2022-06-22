using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    public float hp = 20f;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.gameObject.CompareTag("Enemy"))
        {
            hp -= 5f;
            if(hp <= 0)
                Destroy(gameObject);
        }
    }

    
}
