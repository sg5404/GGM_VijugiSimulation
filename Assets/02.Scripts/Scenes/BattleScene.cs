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
            // ��ư ���� ���ߵ�
            SetInfoText($"{_playerPokemon.Name}��(��) ������ �ұ�?", 0.4f);
        }
        else
        {
            // TODO : Enemy Action
            // �ణ�� ������ �� ����
            Debug.Log("Enemy Action!");
            isPlayerTurn = !isPlayerTurn;
        }

        // Debug Code
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SetInfoText($"{_playerPokemon.Name}��(��) ������ �ұ�?", 0.4f);
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
                // ��� â ����
                Debug.Log("��Ʋ!");
                // TODO : ��� ���� ����Ʈ�� ������ �ֱ�
                SetActionPanel((int)ActionType.Fight);
                break;
            case ActionType.Pokemon:
                // ���ϸ� â ����
                Debug.Log("�뿹 ��ü");
                SetActionPanel((int)ActionType.Pokemon);
                break;
            case ActionType.Item:
                // ������ â ����
                Debug.Log("������ ���");
                SetActionPanel((int)ActionType.Item);
                break;
            case ActionType.Run:
                // ���� �������� ���� �Ǵ�
                // ��Ʋ �����Ű��
                StartCoroutine(BattleEnd(1.5f));
                Debug.Log("!����");
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
