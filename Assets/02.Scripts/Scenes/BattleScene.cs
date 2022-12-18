using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
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

    [SerializeField]
    private GameObject _inputIgnorePanel;

    private GameInfo _gameInfo;
    public GameInfo GameInfo => _gameInfo;

    private AgentInfo _playerInfo;
    public AgentInfo PlayerInfo => _playerInfo;
    private AgentInfo _enemyInfo;
    public AgentInfo EnemyInfo => _enemyInfo;
    private Pokemon _playerPokemon = new Pokemon();
    public Pokemon PlayerPokemon => _playerPokemon;
    private Pokemon _enemyPokemon = new Pokemon();
    public Pokemon EnemyPokemon => _enemyPokemon;

    private bool _isPlayerTurn = false;
    public bool IsPlayerTurn => _isPlayerTurn;
    private bool _isBattleStart = false;

    private Poolable _enemyPokemonPrefab;
    private Poolable _playerPokemonPrefab;

    private Coroutine _battleCoroutine = null;

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
        }
        _enemyInfoPanel.SetActiveExpBar(false);
        _enemyInfoPanel.SetActiveHpText(false);
        _playerPokemon = _playerInfo.PokemonList[0];

        SetInfoText("");
        UpdateUI();

        AllClosePanel();

        SetTurn();

        StartCoroutine(SpawnPokemon());

        // 스폰 이펙트 끝난다음으로 미루기

        _battleCoroutine = StartCoroutine(BattleCoroutine());
    }

    public void UpdateUI()
    {
        _enemyInfoPanel.SetInfo(SetInfo(_enemyPokemon));
        _playerInfoPanel.SetInfo(SetInfo(_playerPokemon));
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
        Vector3 scale = Vector3.one;
        scale *= pokemon.Info.scale switch
        {
            Define.PokeScale.Small => isEnemy == true ? 5 : 4,
            Define.PokeScale.Medium => isEnemy == true ? 4 : 3,
            Define.PokeScale.Large => isEnemy == true ? 3 : 2,
            _ => isEnemy == true ? 5 : 4,
        };
        prefab.transform.localScale = scale;

        SpawnEffect(
            ref prefab,
            () =>
            {
                if(isEnemy == true)
                {
                    _isBattleStart = true;
                }
            });
    }

    private void SpawnEffect(ref Poolable prefab, Action action = null)
    {
        // 이펙트
        prefab.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack).From();

        action?.Invoke();
    }

    private void DestroyEffect(ref Poolable prefab, Action action = null)
    {
        // 이펙트

        //prefab.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack);
        prefab.transform.localScale = Vector3.one;
        Managers.Pool.Push(prefab); // 이럼 안되지
        SpawnPokemon(_playerPokemon, ref _playerPokemonPrefab, _playerPokemonPos, false);

        action?.Invoke();
    }

    private UIInfo SetInfo(Pokemon pokemon)
    {
        UIInfo info = new UIInfo();
        info.name = pokemon.Name;
        info.level = pokemon.Level;
        info.hp = pokemon.Hp;
        info.maxHp = pokemon.MaxHp;
        info.exp = pokemon.CurExp;
        info.maxExp = pokemon.MaxExp;

        return info;
    }

    //private void SetInfoText(string msg, float duraction = 0.8f, bool isClear = true, Action action = null) // 뭔가 문제 있음
    //{
    //    if (_infoText.text == msg) return;

    //    //_infoText.DOKill();

    //    if(isClear == true)
    //    {
    //        _infoText.text = "";
    //    }
    //    _infoText.DOText(msg, duraction).OnComplete(() => action?.Invoke());
    //}

    public void SetInfoText(string msg, Action action = null)
    {
        _infoText.text = msg;
        action?.Invoke();
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
    }

    public void ChangeTurn()
    {
        ChangeTurn(!_isPlayerTurn);
    }

    private IEnumerator BattleCoroutine()
    {
        _inputIgnorePanel.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (_gameInfo.isWildPokemon)
        {
            SetInfoText($"앗! 야생의 {_enemyPokemon.Name}이 나타났다!");
        }
        else
        {
            SetInfoText($"{_enemyInfo.Name}이 승부를 걸어왔다!");
        }

        while (true)
        {
            if (_isBattleStart == false) yield return null;
            _inputIgnorePanel.SetActive(false);

            yield return new WaitUntil(() => _isPlayerTurn == false);
            _inputIgnorePanel.SetActive(true);

            // TODO : Enemy Action
            yield return new WaitForSeconds(0.5f);
            SetInfoText($"{_enemyPokemon.Name}의 차례!");

            yield return new WaitForSeconds(1f);
            SetInfoText($"{_enemyPokemon.Name}은 (사용한 스킬 이름)를 사용했다.");

            ChangeTurn();

            yield return new WaitForSeconds(0.5f);
            SetInfoText($"{_playerPokemon.Name}은(는) 무엇을 할까?");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

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
                // TODO : 기술 사용시 이펙트와 데미지 주기
                SetActionPanel((int)ActionType.Fight);
                _actionPanelList[(int)ActionType.Fight].GetComponent<SkillPanel>().SetSkill(_playerPokemon.SkillList);
                break;
            case ActionType.Pokemon:
                // 포켓몬 창 열기
                SetActionPanel((int)ActionType.Pokemon);
                _actionPanelList[(int)ActionType.Pokemon].GetComponent<PokemonPanel>().SetPokeom(_playerInfo.PokemonList);
                break;
            case ActionType.Item:
                // 아이테 창 열기
                SetActionPanel((int)ActionType.Item);
                _actionPanelList[(int)ActionType.Item].GetComponent<ItemPanel>().SetList(_playerInfo.itemList);
                break;
            case ActionType.Run:
                // 도망 가능한지 여부 판단
                // 배틀 종료시키기
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
                    SetInfoText("무사히 도망쳤다.");
                    StartCoroutine(BattleEnd(1.5f));
                }
                else
                {
                    SetInfoText("도망칠 수 없다!");
                }
                break;
        }
    }

    private IEnumerator BattleVictory()
    {
        StopCoroutine(_battleCoroutine);
        _isPlayerTurn = false;
        _isBattleStart = false;

        SetInfoText($"{_enemyPokemon.Name}은 쓰러졌다.");
        yield return new WaitForSeconds(0.5f);

        _gameInfo.wildPokemon = null;
        _playerInfo.PokemonList[0] = _playerPokemon;
        _gameInfo.PlayerInfo = _playerInfo;
        Managers.Save.SaveJson(_gameInfo);

        Managers.Scene.LoadScene(Define.Scene.Map);
    }

    public void Attack(SkillSO skill)
    {
        bool isCritical = Random.value <= 0.06f ? true : false;
        DamageType type = _enemyPokemon.Damage(skill.power, _playerPokemon.Attack, skill.type, isCritical);

        SetInfoText($"{PlayerPokemon.Name}의 {skill.skillName}!");
        StartCoroutine(ChangeTurnCoroutine(type));
        UpdateUI();
        AllClosePanel();
    }

    private IEnumerator ChangeTurnCoroutine(DamageType type)
    {
        switch (type)
        {
            case DamageType.GREAT:
                yield return new WaitForSeconds(0.5f);
                SetInfoText("효과가 굉장했다.");
                break;
            case DamageType.MEDIOCRE:
                break;
            case DamageType.NOTGOOD:
                yield return new WaitForSeconds(0.5f);
                SetInfoText("효과가 별로다.");
                break;
            case DamageType.NO:
                yield return new WaitForSeconds(0.5f);
                SetInfoText("효과가 없다.");
                break;
        }

        if (_enemyPokemon.Hp <= 0)
        {
            int exp = Mathf.Max((_enemyPokemon.Level - _playerPokemon.Level) * (_playerPokemon.Level / 2), 1) + 5;
            _playerPokemon.AddExp(exp);

            StartCoroutine(BattleVictory());
            yield break;
            // 경험치 공식?
            // MAX((B2 - A2) * (A2 / 2), 1) + 5
        }
        ChangeTurn();
    }

    public void SwapPokemon(int fIdx, int sIdx)
    {
        if (fIdx < 0 || sIdx < 0 || fIdx > 5 || sIdx > 5) return;
        if (fIdx == sIdx) return;
        if (_playerInfo.PokemonList[fIdx] == null || _playerInfo.PokemonList[sIdx] == null) return;

        Pokemon temp = _playerInfo.PokemonList[fIdx];
        _playerInfo.PokemonList[fIdx] = _playerInfo.PokemonList[sIdx];
        _playerInfo.PokemonList[sIdx] = temp;

        _playerPokemon = _playerInfo.PokemonList[0];

        _playerInfoPanel.SetInfo(SetInfo(_playerPokemon));

        // 교체 이펙트

        DestroyEffect(ref _playerPokemonPrefab);
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
        SetInfoText("무사히 도망쳤다.");
        yield return new WaitForSeconds(delay);
        _gameInfo.isWildPokemon = false;
        _gameInfo.wildPokemon = null;
        _gameInfo.PlayerInfo = _playerInfo;
        Managers.Save.SaveJson(_gameInfo);
        Managers.Scene.LoadScene(Define.Scene.Map);
    }


    public override void Clear()
    {
        _isPlayerTurn = false;
        _isBattleStart = false;

        _playerPokemon = null;
        _enemyPokemon = null;

        StopCoroutine(_battleCoroutine);

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
