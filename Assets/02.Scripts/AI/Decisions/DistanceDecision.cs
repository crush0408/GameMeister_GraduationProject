using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDecision : AIDecision
{
    [Range(0.1f, 30f)]
    public float distance = 5f;

    public override bool MakeADecision()
    {
        float calc = Vector3.Distance(_enemyBrain.target.transform.position, transform.position); 
            
        return calc < distance;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distance);
            Gizmos.color = Color.white;
        }
    }
#endif

}
