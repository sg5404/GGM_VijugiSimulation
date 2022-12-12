using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ActionType
{
    None,
    Fight,
    Pokemon,
    Item,
    Run,
}

public class BattleScene : BaseScene
{
    [SerializeField]
    private InfoBar _playerInfoPanel;
    [SerializeField]
    private InfoBar _enemyInfoPanel;

    [SerializeField]
    private Text _infoText;

    private GameInfo _gameInfo;

    private AgentInfo _playerInfo;
    private AgentInfo _enemyInfo;
    private Pokemon _wildPokemon = new Pokemon();

    private bool isPlayerTurn = false;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Battle;

        _gameInfo = Managers.Save.LoadJsonFile<GameInfo>();
        _playerInfo = _gameInfo.PlayerInfo;
        if (_gameInfo.isWildPokemon) 
        {
            _wildPokemon = _gameInfo.wildPokemon;
            _enemyInfoPanel.SetInfo(SetInfo(_wildPokemon));
        }
        else
        {
            _enemyInfo = _gameInfo.EnemyInfo;
            _enemyInfoPanel.SetInfo(SetInfo(_enemyInfo.PokemonList[0]));
        }
        _enemyInfoPanel.SetActiveExpBar(!_gameInfo.isWildPokemon);
        _playerInfoPanel.SetInfo(SetInfo(_playerInfo.PokemonList[0]));

        
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

    private void SetInfoText(string msg, float duraction = 0.8f, bool isClear = true)
    {
        if(isClear == true)
        {
            _infoText.text = "";
        }
        _infoText.DOText(msg, duraction);
    }

    private void SetTurn()
    {
        int playerSpeed;
        int enemSpeed;

        if(_gameInfo.isWildPokemon == true)
        {
            enemSpeed = _gameInfo.wildPokemon.Speed;
        }
        else
        {
            enemSpeed = _gameInfo.EnemyInfo.PokemonList[0].Speed;
        }

        playerSpeed = _gameInfo.PlayerInfo.PokemonList[0].Speed;

        isPlayerTurn = playerSpeed > enemSpeed ? true : false;
    }

    private void Update()
    {
        if (isPlayerTurn)
        {
            // TODO : Player Action.
            // 버튼 연걸 알잘딱
        }
        else
        {
            // TODO : Enemy Action
            Debug.Log("Enemy Action!");
            isPlayerTurn = !isPlayerTurn;
        }
    }

    public void PlayerAction(int index)
    {
        PlayerAction((ActionType)index);
    }


    private void PlayerAction(ActionType type)
    {
        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Fight:
                // 기술 창 열기
                Debug.Log("배틀!");
                // TODO : 기술 사용시 이펙트와 데미지 주기
                break;
            case ActionType.Pokemon:
                // 포켓몬 창 열기
                Debug.Log("노예 교체");
                break;
            case ActionType.Item:
                // 아이테 창 열기
                Debug.Log("아이템 사용");
                break;
            case ActionType.Run:
                // 도망 가능한지 여부 판단
                // 배틀 종료시키기
                StartCoroutine(BattleEnd(1.5f));
                Debug.Log("!도망");
                break;
        }
    }

    private IEnumerator BattleEnd(float delay)
    {
        SetInfoText("무사히 도망쳤다.");
        yield return new WaitForSeconds(delay);
        _gameInfo.isWildPokemon = false;
        _gameInfo.wildPokemon = null;
        Managers.Save.SaveJson(_gameInfo);
        Managers.Scene.LoadScene(Define.Scene.Map);
    }

    public override void Clear()
    {
        
    }
}
