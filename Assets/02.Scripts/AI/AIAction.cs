using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{

    [SerializeField] protected EnemyBrain _enemyBrain = null;

    private void Awake()
    {
        _enemyBrain = GetComponentInParent<EnemyBrain>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        //do Nothing
    }

    public abstract void TakeAction();
}
