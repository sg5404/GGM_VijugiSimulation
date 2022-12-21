using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleAction : AIState
{
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = _aiBrain.GetComponentInParent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        _agent.isStopped = true;
    }

    public override void OnStateLeave()
    {
        _agent.isStopped = false;
    }

    public override void TakeAAction()
    {
        
    }
}
