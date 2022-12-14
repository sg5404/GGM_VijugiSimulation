using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    [SerializeField]
    private Transform _playerPokemonPos;
    [SerializeField]
    private Transform _enemyPokemonPos;

    private GameInfo _gameInfo;

    private AgentInfo _playerInfo;
    public AgentInfo PlayerInfo => _playerInfo;
    private AgentInfo _enemyInfo;
    public AgentInfo EnemyInfo => _enemyInfo;
    private Pokemon _playerPokemon = new Pokemon();
    private Pokemon _enemyPokemon = new Pokemon();

    private bool _isPlayerTurn = false;
    private bool _isBattleStart = false;

    private Poolable _enemyPokemonPrefab;
    private Poolable _playerPokemonPrefab;

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

        AllClosePanel();

        SetTurn();

        StartCoroutine(SpawnPokemon());
    }

    private IEnumerator SpawnPokemon()
    {
        yield return new WaitForSeconds(0.1f);
        SpawnPokemon(_enemyPokemon, ref _enemyPokemonPrefab, _enemyPokemonPos, true);
        
        yield return new WaitForSeconds(1f);
        SpawnPokemon(_playerPokemon, ref _playerPokemonPrefab, _playerPokemonPos, false);
    }

    private void SpawnPokemon(Pokemon pokemon, ref Poolable prefab, Transform pos, bool isEnemy)
    {
        prefab = Managers.Resource.Instantiate($"Pokemon/{pokemon.Info.prefab.name}").GetComponent<Poolable>();
        prefab.transform.localPosition = pos.localPosition;
        prefab.transform.localRotation = pos.localRotation;
        Vector3 scale = prefab.transform.localScale;
        scale *= pokemon.Info.scale switch
        {
            Define.PokeScale.Small => isEnemy == true ? 5 : 4,
            Define.PokeScale.Medium => isEnemy == true ? 4 : 3,
            Define.PokeScale.Large => isEnemy == true ? 3 : 2,
            _ => isEnemy == true ? 5 : 4,
        };
        prefab.transform.localScale = scale;

        SpawnEffect(ref prefab, isEnemy == true ? null : () => _isBattleStart = true);
    }

    private void SpawnEffect(ref Poolable prefab, Action action = null)
    {
        // ����Ʈ
        prefab.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack).From();

        action?.Invoke();
    }

    private void DestroyEffect(ref Poolable prefab, Action action = null)
    {
        // ����Ʈ

        action?.Invoke();
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

    private void SetInfoText(string msg, float duraction = 0.8f, bool isClear = true) // ���� ���� ����
    {
        _infoText.DOKill();

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
                ChangeTurn(true);
            }
            else
            {
                ChangeTurn(false);
            }
        }
        else if(playerSpeed > enemSpeed)
        {
            ChangeTurn(true);
        }
        else
        {
            ChangeTurn(false);
        }
    }

    public void ChangeTurn(bool value)
    {
        _isPlayerTurn = value;

        if(value == true)
        {
            SetInfoText($"{_playerPokemon.Name}��(��) ������ �ұ�?", 0.4f);
        }
        else
        {
            SetInfoText($"{_playerPokemon.Name}��(��) ������ �ұ�?", 0.4f);
        }
    }

    public void ChangeTurn()
    {
        ChangeTurn(!_isPlayerTurn);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }

        if (_isPlayerTurn)
        {
            // TODO : Player Action.
            // ��ư ���� ���ߵ�
            //SetInfoText($"{_playerPokemon.Name}��(��) ������ �ұ�?", 0.4f, false);
        }
        else
        {
            // TODO : Enemy Action
            // �ణ�� ������ �� ����
            Debug.Log("Enemy Action!");
            ChangeTurn();
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
                _actionPanelList[(int)ActionType.Fight].GetComponent<SkillPanel>().SetSkill(_playerPokemon.SkillList);
                SetActionPanel((int)ActionType.Fight);
                break;
            case ActionType.Pokemon:
                // ���ϸ� â ����
                Debug.Log("�뿹 ��ü");
                _actionPanelList[(int)ActionType.Pokemon].GetComponent<PokemonPanel>().SetPokeom(_playerInfo.PokemonList);
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
                bool isRun = false;
                if(_playerPokemon.Level > _enemyPokemon.Level)
                {
                    isRun = true;
                }
                else if(_playerPokemon.Level < _enemyPokemon.Level)
                {
                    isRun = false;
                }
                else
                {
                    float rand = Random.value;
                    if(rand >= 0.5f)
                    {
                        isRun = true;
                    }
                    else
                    {
                        isRun = false;
                    }
                }
                if(isRun == true)
                {
                    Debug.Log("!���� ����");
                    StartCoroutine(BattleEnd(1.5f));
                }
                else
                {
                    Debug.Log("!���� ����");
                    SetInfoText("����ĥ �� ����!", 0.5f);
                }
                break;
        }
    }

    public void SwapPokemon(int fIdx, int sIdx) // 6�� ���͹����µ�...
    {
        if (fIdx < 0 || sIdx < 0 || fIdx > 5 || sIdx > 5) return;
        if (fIdx == sIdx) return;
        if (_playerInfo.PokemonList[fIdx] == null || _playerInfo.PokemonList[sIdx] == null) return;

        Pokemon temp = _playerInfo.PokemonList[fIdx];
        _playerInfo.PokemonList[fIdx] = _playerInfo.PokemonList[sIdx];
        _playerInfo.PokemonList[sIdx] = temp;
        // ��ü ����Ʈ
    }

    public void SetActionPanel(int index)
    {
        AllClosePanel();

        _actionPanelList[index]?.SetActive(true);
    }

    public void AllClosePanel()
    {
        foreach (GameObject go in _actionPanelList)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
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
        if(_playerPokemonPrefab != null)
        {
            _playerPokemonPrefab.transform.localScale = Vector3.one;
            Managers.Pool.Push(_playerPokemonPrefab);
        }

        if(_enemyPokemonPrefab != null)
        {
            _enemyPokemonPrefab.transform.localScale = Vector3.one;
            Managers.Pool.Push(_enemyPokemonPrefab);
        }
    }
}
