using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMagicEffectScript : PoolableMono
{
    public override void Reset()
    {

        
    }
    public void PushFunc()
    {
        PoolManager.Instance.Push(this);
    }
}
