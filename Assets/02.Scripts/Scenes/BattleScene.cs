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
    PokemonChoice,
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

    private int _throwIndex = -1;

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
                GameObject effect = Managers.Resource.Instantiate("Effect/SpawnEffect");
                effect.transform.position = pos.transform.position;
            });
    }

    private void SpawnEffect(ref Poolable prefab, Action action = null)
    {
        prefab.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack).From();

        action?.Invoke();
    }

    private void DestroyEffect(ref Poolable prefab, Action action = null)
    {
        // �����ұ�? ��������...

        //prefab.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack);
        prefab.transform.localScale = Vector3.one;
        Managers.Pool.Push(prefab);
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

    //private void SetInfoText(string msg, float duraction = 0.8f, bool isClear = true, Action action = null)
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

        enemSpeed = _enemyPokemon.Speed;

        playerSpeed = _playerPokemon.Speed;

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
            SetInfoText($"�߻��� {_enemyPokemon.Name}�� ��Ÿ����!");
        }
        else
        {
            SetInfoText($"{_enemyInfo.Name}�� �ºθ� �ɾ�Դ�!");
        }

        yield return new WaitForSeconds(3f);

        while (true)
        {
            if (_isBattleStart == false) yield return null;
            _inputIgnorePanel.SetActive(false);

            yield return new WaitUntil(() => _isPlayerTurn == false);
            _inputIgnorePanel.SetActive(true);

            // TODO : Enemy Action
            yield return new WaitForSeconds(0.5f);
            SetInfoText($"{_enemyPokemon.Name}�� ��!");

            yield return new WaitForSeconds(1f);
            int rand = 0;
            int idx = 0;
            for(int i = 0; i < 4; i++)
            {
                if (_enemyPokemon.SkillList[i] != null)
                {
                    idx++;
                }
            }
            rand = Random.Range(0, idx);
            int cri = Random.Range(0, 101);
            SkillSO skill = _enemyPokemon.SkillList[rand];
            if (skill != null)
            {
                SetInfoText($"{_enemyPokemon.Name}�� {skill.skillName}!");
                if (cri <= skill.accuracyRate)
                {
                    //bool isCritical = Random.value <= 0.06f ? true : false;
                    bool isCritical = Random.value <= 0.1f ? true : false;
                    DamageType type = _playerPokemon.Damage(skill.power, _enemyPokemon.Attack, skill.type, isCritical);
                    GameObject hit = Managers.Resource.Instantiate("Effect/Hit");
                    hit.transform.position = _playerPokemonPos.position + Vector3.up;

                    StartCoroutine(ChangeTurnCoroutine(type));
                    UpdateUI();
                }
                else
                {
                    SetInfoText($"{_enemyPokemon.Name}�� {skill.skillName}�� ��������!");
                    ChangeTurn();
                }
            }
            else
            {
                SetInfoText($"{_enemyPokemon.Name}�� �Ұ� ����...");
                ChangeTurn();
            }

            yield return new WaitForSeconds(1f);
            SetInfoText($"{_playerPokemon.Name}�� ��!");
            yield return new WaitForSeconds(0.5f);
            SetInfoText($"{_playerPokemon.Name}�� ������ �ұ�?");
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
                SetActionPanel((int)ActionType.Fight);
                _actionPanelList[(int)ActionType.Fight].GetComponent<SkillPanel>().SetSkill(_playerPokemon.SkillList);
                break;
            case ActionType.Pokemon:
                SetActionPanel((int)ActionType.Pokemon);
                _actionPanelList[(int)ActionType.Pokemon].GetComponent<PokemonPanel>().SetPokeom(_playerInfo.PokemonList);
                break;
            case ActionType.Item:
                SetActionPanel((int)ActionType.Item);
                _actionPanelList[(int)ActionType.Item].GetComponent<ItemPanel>().SetList(_playerInfo.itemList);
                _actionPanelList[(int)ActionType.Item].GetComponent<ItemPanel>().AddAllItemEvent(ThrowMonsterball);
                break;
            case ActionType.Run:
                bool isRun = false;
                if(_playerPokemon.Level > _enemyPokemon.Level)
                {
                    isRun = true;
                }
                else if(_playerPokemon.Level < _enemyPokemon.Level)
                {
                    float rand = Random.value;
                    if (rand <= 0.2f)
                        isRun = true;
                    else
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
                    StartCoroutine(BattleEnd(1.5f));
                }
                else
                {
                    SetInfoText("����ĥ �� ����!");
                }
                break;
        }
    }

    private void ThrowMonsterball()
    {
        StartCoroutine(ThrowMonsterballCoroutine());
        Debug.Log("�� ������");
    }

    private IEnumerator ThrowMonsterballCoroutine()
    {
        AllClosePanel();

        SetInfoText($"{_playerInfo.Name}�� ���ͺ��� ������!");
        
        Poolable monsterball =  Managers.Resource.Instantiate("Pokemon/�����").GetComponent<Poolable>();
        monsterball.transform.position = _playerPokemonPos.position;
        monsterball.transform.DOJump(_enemyPokemonPos.position, 6, 1, 0.7f).OnComplete(() =>
        {
            _enemyPokemonPrefab.transform.position = Vector3.one;
            Managers.Pool.Push(_enemyPokemonPrefab);
        });
        _actionPanelList[(int)ActionType.Item].GetComponent<ItemPanel>().RemoveItem(0, 1); // 2�� ��
        yield return new WaitForSeconds(3f);
        int rand = Random.Range(0, 101);
        int ra = _enemyPokemon.Info.rarity switch
        {
            Define.PokeRarity.Common => 40,
            Define.PokeRarity.Rare => 30,
            Define.PokeRarity.unique => 20,
            Define.PokeRarity.Legendary => 1,
            _ => 40,
        };

        if (rand <= ra)
        {
            // ��ȹ ����
            // ����Ʈ
            monsterball.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBack);
            Debug.Log("��ȹ ����!");
            // �� ���� ���ϸ� ����Ʈ�� �ֱ�
            for(int i = 0; i < 6; i++)
            {
                if (_playerInfo.PokemonList[i].Info == null)
                {
                    _playerInfo.PokemonList[i] = _enemyPokemon;

                    SetInfoText($"�ų���!\n{_enemyPokemon.Name}��(��) ����Ҵ�!");

                    StopCoroutine(_battleCoroutine);
                    _isPlayerTurn = false;
                    _isBattleStart = false;

                    yield return new WaitForSeconds(0.5f);

                    _gameInfo.wildPokemon = null;
                    _playerInfo.PokemonList[0] = _playerPokemon; // ����ġ�� �ȳѾ� ����.
                    _gameInfo.PlayerInfo = _playerInfo;
                    Managers.Save.SaveJson(_gameInfo);

                    Managers.Pool.Push(monsterball);
                    Managers.Scene.LoadScene(Define.Scene.Map);
                    break;
                }
            }

            // ������� ������ ���ϸ��� ��ã���ϱ� ��ü�ϱ�
            Debug.Log("���� ���� ����");
            SetInfoText("���� ���ϸ��� �����ϼ���.");
            _throwIndex = -1;
            _actionPanelList[(int)ActionType.PokemonChoice].SetActive(true);
            _actionPanelList[(int)ActionType.PokemonChoice].GetComponent<PokemonChoicePanel>().SetPokeom(_playerInfo.PokemonList);
            yield return new WaitUntil(() => _throwIndex != -1);
            _playerInfo.PokemonList[_throwIndex] = _enemyPokemon;

            StopCoroutine(_battleCoroutine);
            _isPlayerTurn = false;
            _isBattleStart = false;

            _gameInfo.wildPokemon = null;
            _playerInfo.PokemonList[0] = _playerPokemon;
            _gameInfo.PlayerInfo = _playerInfo;
            Managers.Save.SaveJson(_gameInfo);

            Managers.Scene.LoadScene(Define.Scene.Map);
        }
        else
        {
            // ����
            Managers.Pool.Push(monsterball);
            SpawnPokemon(_enemyPokemon, ref _enemyPokemonPrefab, _enemyPokemonPos, true);
            SetInfoText($"�ƽ���!\n���ݸ� �� �ϸ� ���� �� �־��µ�!");
            
        }
        yield return new WaitForSeconds(0.5f);
        ChangeTurn();
    }

    public void ReturnThrowIndex(int i)
    {
        _throwIndex = i;
    }

    private IEnumerator BattleVictory()
    {
        StopCoroutine(_battleCoroutine);
        _isPlayerTurn = false;
        _isBattleStart = false;

        SetInfoText($"{_enemyPokemon.Name}�� ����Ʈ�ȴ�!");
        yield return new WaitForSeconds(1f);
        int exp = Mathf.Max((_enemyPokemon.Level - _playerPokemon.Level) * (_playerPokemon.Level / 2), 1) + 5;
        _playerPokemon.AddExp(exp);
        UpdateUI();
        yield return new WaitForSeconds(1f);
        SetInfoText($"{exp}�� ����ġ�� �����.");
        yield return new WaitForSeconds(1f);
        _gameInfo.wildPokemon = null;
        _playerInfo.PokemonList[0] = _playerPokemon;
        _gameInfo.PlayerInfo = _playerInfo;
        Managers.Save.SaveJson(_gameInfo);

        Managers.Scene.LoadScene(Define.Scene.Map);
    }

    private IEnumerator BattleDefeat()
    {
        StopCoroutine(_battleCoroutine);
        _isPlayerTurn = false;
        _isBattleStart = false;

        SetInfoText($"{_playerInfo.Name}��(��) �й��g��.\n{_playerInfo.Name}��(��) �� ���� ����������.");
        yield return new WaitForSeconds(0.5f);

        // Fade Out ȿ��
        // ���� ���� ����?

        Managers.Save.DeleteFile();
        Managers.Scene.LoadScene(Define.Scene.Menu);
    }

    public void Attack(SkillSO skill)
    {
        int rand = Random.Range(0, 101);
        if (rand <= skill.accuracyRate)
        {
            bool isCritical = Random.value <= 0.06f ? true : false;
            DamageType type = _enemyPokemon.Damage(skill.power, _playerPokemon.Attack, skill.type, isCritical);
            GameObject hit = Managers.Resource.Instantiate("Effect/Hit");
            hit.transform.position = _enemyPokemonPos.position + Vector3.up;
            hit.transform.position = _enemyPokemonPos.position + Vector3.up;

            SetInfoText($"{PlayerPokemon.Name}�� {skill.skillName}!");
            StartCoroutine(ChangeTurnCoroutine(type));
            UpdateUI();
            AllClosePanel();
        }
        else
        {
            SetInfoText($"{PlayerPokemon.Name}�� {skill.skillName}�� ��������!");
            ChangeTurn();
        }
    }

    private IEnumerator ChangeTurnCoroutine(DamageType type)
    {
        switch (type)
        {
            case DamageType.GREAT:
                yield return new WaitForSeconds(0.5f);
                SetInfoText("ȿ���� �����ߴ�!");
                break;
            case DamageType.MEDIOCRE:
                break;
            case DamageType.NOTGOOD:
                yield return new WaitForSeconds(0.5f);
                SetInfoText("ȿ���� ���δ�.");
                break;
            case DamageType.NO:
                yield return new WaitForSeconds(0.5f);
                SetInfoText("ȿ���� ����.");
                break;
        }

        if (_enemyPokemon.Hp <= 0)
        {
            // �׳� ��������
            StartCoroutine(BattleVictory());
            yield break;
            // MAX((B2 - A2) * (A2 / 2), 1) + 5
        }
        else if(_playerPokemon.Hp <= 0)
        {
            // ���� ���ϸ��� �� ���ٸ�
            //StartCoroutine(BattleDefeat());
            int idx = -1;
            for(int i = 0; i < 6; i++)
            {
                if (_playerInfo.PokemonList[i] != null && _playerInfo.PokemonList[i].Hp > 0)
                {
                    idx = i;
                    break;
                }
            }

            if(idx != -1) // ���ϸ��� �ִ�
            {
                SetInfoText($"�����߾� {_playerPokemon.Name}");
                SwapPokemon(0, idx);
                SetInfoText($"����! {_playerPokemon.Name}!");
                SetTurn();
            }
            else // ���ϸ��� ����.
            {
                StartCoroutine(BattleDefeat());
            }
            yield break;
            // ���� ���ϸ��� �ִٸ�
            // SwapPokemon(0, 1); // 1�� ������ ���� ���� �ε����� ���� ���ϸ����� ��ü
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
        StopCoroutine(_battleCoroutine);

        SetInfoText("������ �����ƴ�.");
        yield return new WaitForSeconds(delay);
        _gameInfo.isWildPokemon = false;
        _gameInfo.wildPokemon = null;
        _playerInfo.PokemonList[0] = _playerPokemon;
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
