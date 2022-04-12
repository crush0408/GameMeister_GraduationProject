using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitDecision : AIDecision
{
    [Range(1f, 5f)]
    public float waitTime = 3f;
    public override bool MakeADecision()
    {
        StartCoroutine(WaitCoroutine());
        return true;
    }
    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
