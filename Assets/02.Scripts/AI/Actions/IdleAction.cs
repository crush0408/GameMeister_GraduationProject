using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public float time = 3f;
    public override void TakeAction()
    {
        _enemyBrain.Stop();
        /*
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
        }
        */
    }
}
