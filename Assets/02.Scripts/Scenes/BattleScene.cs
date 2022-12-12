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

    [SerializeField]
    private List<GameObject> _actionPanelList;

    private GameInfo _gameInfo;

    private AgentInfo _playerInfo;
    private AgentInfo _enemyInfo;
    private Pokemon _playerPokemon = new Pokemon();
    private Pokemon _enemyPokemon = new Pokemon();

    private bool isPlayerTurn = false;



    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Battle;

        _gameInfo = Managers.Save.LoadJsonFile<GameInfo>();
        _playerInfo = _gameInfo.PlayerInfo;
        if (_gameInfo.isWildPokemon) 
        {
            _enemyPokemon = _gameInfo.wildPokemon;
            _enemyInfoPanel.SetInfo(SetInfo(_enemyPokemon));
        }
        else
        {
            _enemyInfo = _gameInfo.EnemyInfo;
            _enemyPokemon = _enemyInfo.PokemonList[0];
            _enemyInfoPanel.SetInfo(SetInfo(_enemyPokemon));
        }
        _enemyInfoPanel.SetActiveExpBar(!_gameInfo.isWildPokemon);
        _playerPokemon = _playerInfo.PokemonList[0];
        _playerInfoPanel.SetInfo(SetInfo(_playerPokemon));

        
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

        if(playerSpeed == enemSpeed)
        {
            float rand = Random.value;
            if(rand >= 0.5f)
            {
                isPlayerTurn = true;
            }
            else
            {
                isPlayerTurn = false;
            }
        }
        else if(playerSpeed > enemSpeed)
        {
            isPlayerTurn = true;
        }
        else
        {
            isPlayerTurn = false;
        }
    }

    private void Update()
    {
        if (isPlayerTurn)
        {
            // TODO : Player Action.
            // 버튼 연걸 알잘딱
            SetInfoText($"{_playerPokemon.Name}은(는) 무엇을 할까?", 0.4f);
        }
        else
        {
            // TODO : Enemy Action
            // 약간의 딜레이 후 공격
            Debug.Log("Enemy Action!");
            isPlayerTurn = !isPlayerTurn;
        }

        // Debug Code
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SetInfoText($"{_playerPokemon.Name}은(는) 무엇을 할까?", 0.4f);
        //}
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
                SetActionPanel((int)ActionType.Fight);
                break;
            case ActionType.Pokemon:
                // 포켓몬 창 열기
                Debug.Log("노예 교체");
                SetActionPanel((int)ActionType.Pokemon);
                break;
            case ActionType.Item:
                // 아이테 창 열기
                Debug.Log("아이템 사용");
                SetActionPanel((int)ActionType.Item);
                break;
            case ActionType.Run:
                // 도망 가능한지 여부 판단
                // 배틀 종료시키기
                StartCoroutine(BattleEnd(1.5f));
                Debug.Log("!도망");
                break;
        }
    }

    public void SetActionPanel(int index)
    {
        foreach(GameObject go in _actionPanelList)
        {
            if(go != null)
            {
                go.SetActive(false);
            }
        }

        _actionPanelList[index]?.SetActive(true);
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
