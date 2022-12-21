using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCodition : AICondition
{
    [SerializeField]
    private float _range;

    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        return Vector3.Distance(_aiBrain.Target.transform.position, this.transform.position) < _range;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, _range);
        Gizmos.color = Color.white;
    }
#endif
}
