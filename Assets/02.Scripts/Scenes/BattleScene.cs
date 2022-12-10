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

        UIInfo info = new UIInfo();
        info.name = _enemyInfo.PokemonList[0].Name;
        //info.level = _enemyInfo.PokemonList[0].Level;
        info.hp = _enemyInfo.PokemonList[0].CurHp;
        info.maxHp = _enemyInfo.PokemonList[0].MaxHp;
        info.exp = _enemyInfo.PokemonList[0].CurExp;
        info.maxExp = _enemyInfo.PokemonList[0].MaxExp;
        _enemyInfoPanel.SetInfo(info);
        _enemyInfoPanel.SetActiveExpBar(!_gameInfo.wildPokemon);
    }

    public override void Clear()
    {
        
    }
}
