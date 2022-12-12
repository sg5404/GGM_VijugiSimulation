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
            // ��ư ���� ���ߵ�
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
                // ��� â ����
                Debug.Log("��Ʋ!");
                // TODO : ��� ���� ����Ʈ�� ������ �ֱ�
                break;
            case ActionType.Pokemon:
                // ���ϸ� â ����
                Debug.Log("�뿹 ��ü");
                break;
            case ActionType.Item:
                // ������ â ����
                Debug.Log("������ ���");
                break;
            case ActionType.Run:
                // ���� �������� ���� �Ǵ�
                // ��Ʋ �����Ű��
                StartCoroutine(BattleEnd(1.5f));
                Debug.Log("!����");
                break;
        }
    }

    private IEnumerator BattleEnd(float delay)
    {
        SetInfoText("������ �����ƴ�.");
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
