using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVCondition : AICondition
{
    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        return true;
    }
}
