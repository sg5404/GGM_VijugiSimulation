using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVCondition : AICondition
{
    private FieldOfView _fov;

    private void Start()
    {
        _fov = _aiBrain.GetComponentInParent<FieldOfView>();
    }

    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        if (_fov == null) return false;

        return _fov.SearchEneny;
    }
}
