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
    private Pokemon _wildPokemon = new Pokemon();

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Battle;

        _gameInfo = Managers.Save.LoadJsonFile<GameInfo>();
        _playerInfo = _gameInfo.PlayerInfo;
        if (_gameInfo.isWildPokemon) 
        {
            _wildPokemon = _gameInfo.wildPokemon;
            //Debug.Log(_wildPokemon.Info); // PokemonInfoSO가 날라감. PokeArea 스크립트에서는 잘 있음
            Debug.Log(_wildPokemon);
            Debug.Log(_wildPokemon.Level);
            _enemyInfoPanel.SetInfo(SetInfo(_wildPokemon));
        }
        else
        {
            _enemyInfo = _gameInfo.EnemyInfo;
            _enemyInfoPanel.SetInfo(SetInfo(_enemyInfo.PokemonList[0]));
        }
        _enemyInfoPanel.SetActiveExpBar(!_gameInfo.isWildPokemon);
        //_playerInfoPanel.SetInfo(SetInfo(_playerInfo.PokemonList[0]));
    }

    private UIInfo SetInfo(Pokemon pokemon)
    {
        UIInfo info = new UIInfo();
        info.name = pokemon.Name;
        info.level = pokemon.Level;
        info.hp = pokemon.Hp;
        info.maxHp = pokemon.Hp;
        info.exp = pokemon.CurExp;
        info.maxExp = pokemon.MaxExp;

        return info;
    }

    public override void Clear()
    {
        
    }
}
