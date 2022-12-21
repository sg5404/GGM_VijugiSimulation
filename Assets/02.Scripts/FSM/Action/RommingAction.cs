using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RommingAction : AIState
{
    [SerializeField]
    private Transform[] _rommingPoint;
    private int _nextRommingIndex = -1;
    private Transform _nexRommingPoint;

    private NavMeshAgent _agent;

    private bool isChange = false;

    private void Start()
    {
        _agent = _aiBrain.GetComponentInParent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        if (isChange) return;
        isChange = true;
        _agent.isStopped = false;

        _nextRommingIndex = (_nextRommingIndex + 1) % _rommingPoint.Length; // 2씩 넘어감 왜지...
        _nexRommingPoint = _rommingPoint[_nextRommingIndex];
    }

    public override void OnStateLeave()
    {
        isChange = false;
    }

    public override void TakeAAction()
    {
        _agent.SetDestination(_nexRommingPoint.position);

        if(_agent.stoppingDistance >= _agent.remainingDistance)
        {
            _aiBrain.ChangeState(_transitionList[0].nextState);
        }
    }
}
