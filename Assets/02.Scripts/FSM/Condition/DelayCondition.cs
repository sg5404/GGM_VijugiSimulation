using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayCondition : AICondition
{
    [SerializeField]
    private float _delay;

    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        return _aiBrain.StateDuractionTime >= _delay;
    }
}
