using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleAction : AIState
{
    private NavMeshAgent _agent;

    private Enemy _enemy;

    private void Start()
    {
        _agent = _aiBrain.GetComponentInParent<NavMeshAgent>();
        _enemy = _aiBrain.GetComponentInParent<Enemy>();
    }

    public override void OnStateEnter()
    {
        _agent.isStopped = true;

        // 카메라 전환!
        // 배틀이다 텍스트 띄우기!

        GameInfo gameInfo = new GameInfo();
        gameInfo.PlayerInfo = _aiBrain.Target.GetComponent<Player>().GetInfo();
        gameInfo.EnemyInfo = _enemy.GetInfo();
        gameInfo.isWildPokemon = false;
        Managers.Save.SaveJson(gameInfo);

        MapScene scene = Managers.Scene.CurrentScene as MapScene;
        scene?.SetText(_enemy.BattleStartText);

    }

    public override void TakeAAction()
    {
        if(_aiBrain.StateDuractionTime >= 1f)
        {
            Managers.Scene.LoadScene(Define.Scene.Battle);
        }
    }
}
