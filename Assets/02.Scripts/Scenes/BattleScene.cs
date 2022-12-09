using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : BaseScene
{
    [SerializeField]
    private InfoBar _playerInfoPanel;
    [SerializeField]
    private InfoBar _enemyInfoPanel;

    private GameInfo _gameInfo;

    private AgentInfo _playerInfo;
    private AgentInfo _enemyInfo;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Battle;

        _gameInfo = Managers.Save.LoadJsonFile<GameInfo>();
        _playerInfo = _gameInfo.PlayerInfo;
        _enemyInfo = _gameInfo.EnemyInfo;


        //_enemyInfoPanel.SetInfo(_enemyInfo.PokemonList[0], )
        _enemyInfoPanel.SetActiveExpBar(!_gameInfo.wildPokemon);
    }

    public override void Clear()
    {
        
    }
}
