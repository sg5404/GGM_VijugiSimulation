using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotCondition : AICondition
{
    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        return false;
    }
}
