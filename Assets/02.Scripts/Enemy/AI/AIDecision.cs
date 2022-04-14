using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected EnemyBrain _enemyBrain = null;

    private void Awake()
    {
        _enemyBrain = GetComponentInParent<EnemyBrain>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        //do nothing here
    }

    public abstract bool MakeADecision();
}
