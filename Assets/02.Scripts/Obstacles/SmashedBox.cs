using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashedBox : MonoBehaviour
{
    public int getHit = 0;

    private void Update()
    {
        if (getHit >= 3)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            MGSound.instance.playEff("BoxHit2");
            getHit++;
        }
    }
}
