using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private EnemyBrain _enemyBrain = null;
    [SerializeField]
    private List<AIAction> _actions = null;
    [SerializeField]
    private List<AITransition> _transitions = null;

    private void Awake()
    {
        _enemyBrain = GetComponentInParent<EnemyBrain>();
    }

    public void UpdateState()
    {
        //해당 스테이트에서 수행해야할 액션을 전부 수행하고
        foreach(AIAction action in _actions)
        {
            action.TakeAction();
        }

        //전이가 일어날 것인지를 체크해야한다.
        foreach(AITransition transition in _transitions)
        {
            bool result = false;
            foreach(AIDecision decsion in transition.decisions )
            {
                result = decsion.MakeADecision();
                if (result == false) break;
            }

            //모든 decision 이 성공한거야
            if(result)
            {
                if(transition.positiveResult != null)
                {
                    _enemyBrain.ChangeToState(transition.positiveResult);
                    return;
                }
            }
            else
            {
                if (transition.negativeResult != null)
                {
                    _enemyBrain.ChangeToState(transition.negativeResult);
                    return;
                }
            }
        }

    }
}
