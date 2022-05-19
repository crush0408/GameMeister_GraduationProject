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
        transform.parent = PoolManager.Instance.gameObject.transform;
        transform.localPosition = Vector3.zero;
        PoolManager.Instance.Push(this);
    }
    public void Shake()
    {
        CameraActionScript.ShakeCam(4,1f,false);
    }
}
