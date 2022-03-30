using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMagicEffectScript : PoolableMono
{
    
    public override void Reset()
    {
        //    this.gameObject.transform.position = 
    }
    public void PushFunc()
    {
        PoolManager.Instance.Push(this);
    }
}
