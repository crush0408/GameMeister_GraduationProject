using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectScript : PoolableMono
{
    public override void Reset()
    {

        
    }
    public void PushFunc()
    {
        PoolManager.Instance.Push(this);
        Debug.Log("Effect");
    }
    public void Shake()
    {
        CameraActionScript.ShakeCam(4,1f,false);
    }
}
