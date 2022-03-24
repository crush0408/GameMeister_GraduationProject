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
        //�ش� ������Ʈ���� �����ؾ��� �׼��� ���� �����ϰ�
        foreach(AIAction action in _actions)
        {
            action.TakeAction();
        }

        //���̰� �Ͼ �������� üũ�ؾ��Ѵ�.
        foreach(AITransition transition in _transitions)
        {
            bool result = false;
            foreach(AIDecision decsion in transition.decisions )
            {
                result = decsion.MakeADecision();
                if (result == false) break;
            }

            //��� decision �� �����Ѱž�
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
