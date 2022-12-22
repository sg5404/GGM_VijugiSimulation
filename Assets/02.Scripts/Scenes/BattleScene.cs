using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½Æ® ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ì·ï¿½ï¿?

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
        // ï¿½ï¿½ï¿½ï¿½Æ®
        prefab.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack).From();

        action?.Invoke();
    }

    private void DestroyEffect(ref Poolable prefab, Action action = null)
    {
        // ¼öÁ¤ÇÒ±î? ±ÍÂúÀºµ¥...

        //prefab.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack);
        prefab.transform.localScale = Vector3.one;
        Managers.Pool.Push(prefab); // ï¿½Ì·ï¿½ ï¿½Èµï¿½ï¿½ï¿½
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

    //private void SetInfoText(string msg, float duraction = 0.8f, bool isClear = true, Action action = null) // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
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
            SetInfoText($"¾ß»ýÀÇ {_enemyPokemon.Name}ÀÌ ³ªÅ¸³µ´Ù!");
        }
        else
        {
            SetInfoText($"{_enemyInfo.Name}°¡ ½ÂºÎ¸¦ °É¾î¿Ô´Ù!");
        }

        yield return new WaitForSeconds(2.5f);

        while (true)
        {
            if (_isBattleStart == false) yield return null;
            _inputIgnorePanel.SetActive(false);

            yield return new WaitUntil(() => _isPlayerTurn == false);
            _inputIgnorePanel.SetActive(true);

            // TODO : Enemy Action
            yield return new WaitForSeconds(0.5f);
            SetInfoText($"{_enemyPokemon.Name}ÀÇ ÅÏ!");

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
                SetInfoText($"{_enemyPokemon.Name}ÀÇ {skill.skillName}!");
                if (cri <= skill.accuracyRate)
                {
                    bool isCritical = Random.value <= 0.06f ? true : false;
                    DamageType type = _playerPokemon.Damage(skill.power, _enemyPokemon.Attack, skill.type, isCritical);

                    StartCoroutine(ChangeTurnCoroutine(type));
                    UpdateUI();
                }
                else
                {
                    SetInfoText($"{_enemyPokemon.Name}ÀÇ {skill.skillName}ºø³ª°¬´Ù!");
                }
            }
            else
            {
                SetInfoText($"{_enemyPokemon.Name}´Â ÇÒ°Ô ¾ø´Ù...");
                ChangeTurn();
            }

            yield return new WaitForSeconds(1f);
            SetInfoText($"{_playerPokemon.Name}ÀÇ ÅÏ!");
            yield return new WaitForSeconds(0.5f);
            SetInfoText($"{_playerPokemon.Name}Àº ¹«¾ùÀ» ÇÒ±î?");
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
                // ï¿½ï¿½ï¿?Ã¢ ï¿½ï¿½ï¿½ï¿½
                // TODO : ï¿½ï¿½ï¿?ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½Æ®ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ö±ï¿½
                SetActionPanel((int)ActionType.Fight);
                _actionPanelList[(int)ActionType.Fight].GetComponent<SkillPanel>().SetSkill(_playerPokemon.SkillList);
                break;
            case ActionType.Pokemon:
                // ï¿½ï¿½ï¿½Ï¸ï¿½ Ã¢ ï¿½ï¿½ï¿½ï¿½
                SetActionPanel((int)ActionType.Pokemon);
                _actionPanelList[(int)ActionType.Pokemon].GetComponent<PokemonPanel>().SetPokeom(_playerInfo.PokemonList);
                break;
            case ActionType.Item:
                // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ Ã¢ ï¿½ï¿½ï¿½ï¿½
                SetActionPanel((int)ActionType.Item);
                _actionPanelList[(int)ActionType.Item].GetComponent<ItemPanel>().SetList(_playerInfo.itemList);
                _actionPanelList[(int)ActionType.Item].GetComponent<ItemPanel>().AddAllItemEvent(ThrowMonsterball);
                break;
            case ActionType.Run:
                // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ç´ï¿½
                // ï¿½ï¿½Æ² ï¿½ï¿½ï¿½ï¿½ï¿½Å°ï¿½ï¿?
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
                    //SetInfoText("ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Æ´ï¿½.");
                    StartCoroutine(BattleEnd(1.5f));
                }
                else
                {
                    SetInfoText("µµ¸ÁÄ¥ ¼ö ¾ø´Ù!");
                }
                break;
        }
    }

    private void ThrowMonsterball()
    {
        StartCoroutine(ThrowMonsterballCoroutine());
    }

    private IEnumerator ThrowMonsterballCoroutine()
    {
        AllClosePanel();
        SetInfoText($"{_playerInfo.Name}ï¿½ï¿½(ï¿½ï¿½) ï¿½ï¿½ï¿½Íºï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½!");
        // Æ÷ÄÏ¸ó º¼·Î ¹Ù²Ù±â
        
        Poolable monsterball =  Managers.Resource.Instantiate("Pokemon/Âî¸®¸®°ø").GetComponent<Poolable>();
        monsterball.transform.position = _playerPokemonPos.position;
        monsterball.transform.DOJump(_enemyPokemonPos.position, 6, 1, 0.7f).OnComplete(() =>
        {
            _enemyPokemonPrefab.transform.position = Vector3.one;
            Managers.Pool.Push(_enemyPokemonPrefab);
        });
        _actionPanelList[(int)ActionType.Item].GetComponent<ItemPanel>().RemoveItem(0, 1);
        yield return new WaitForSeconds(3f); // ï¿½ï¿½ï¿½ï¿½Ï¸é¼?ï¿½ï¿½ï¿½Ï¸ï¿½ ï¿½ï¿½ ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½
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
            // Æ÷È¹ ¼º°î
            // ÀÌÆåÆ®
            monsterball.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBack);
            Debug.Log("Æ÷È¹ ¼º°ø!");
            // ³» ³²´Â Æ÷ÄÏ¸ó ¸®½ºÆ®¿¡ ³Ö±â
            for(int i = 0; i < 6; i++)
            {
                if (_playerInfo.PokemonList[i].Info == null)
                {
                    _playerInfo.PokemonList[i] = _enemyPokemon;

                    SetInfoText($"½Å³­´Ù!\n{_enemyPokemon.Name}À»(¸¦) Æ÷È¹Çß´Ù!");

                    StopCoroutine(_battleCoroutine);
                    _isPlayerTurn = false;
                    _isBattleStart = false;

                    yield return new WaitForSeconds(0.5f);

                    _gameInfo.wildPokemon = null;
                    _playerInfo.PokemonList[0] = _playerPokemon; // °æÇèÄ¡°¡ ¾È³Ñ¾î °£´Ù.
                    _gameInfo.PlayerInfo = _playerInfo;
                    Managers.Save.SaveJson(_gameInfo);

                    Managers.Pool.Push(monsterball);
                    Managers.Scene.LoadScene(Define.Scene.Map);
                    break;
                }
            }

            // ¿©±â±îÁö ¿ÔÀ¸¸é Æ÷ÄÏ¸óÀÌ ²ËÃ£À¸´Ï±î ±³Ã¼ÇÏ±â
            Debug.Log("³²´Â °ø°£  ¾øÀ½");
        }
        else
        {
            // ½ÇÆÐ
            Managers.Pool.Push(monsterball);
            SpawnPokemon(_enemyPokemon, ref _enemyPokemonPrefab, _enemyPokemonPos, true);
            SetInfoText($"¾Æ¹«°Íµµ ¸øÇÏÁê?\n½î EZÇÏÁÒ.");
            
        }
        yield return new WaitForSeconds(0.5f);
        ChangeTurn();
    }

    private IEnumerator BattleVictory()
    {
        StopCoroutine(_battleCoroutine);
        _isPlayerTurn = false;
        _isBattleStart = false;

        SetInfoText($"{_enemyPokemon.Name}ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½.");
        yield return new WaitForSeconds(0.5f);

        _gameInfo.wildPokemon = null;
        _playerInfo.PokemonList[0] = _playerPokemon;
        Debug.Log(_playerPokemon.CurExp);
        Debug.Log(_playerInfo.PokemonList[0].CurExp);
        _gameInfo.PlayerInfo = _playerInfo;
        Managers.Save.SaveJson(_gameInfo);

        Managers.Scene.LoadScene(Define.Scene.Map);
    }

    private IEnumerator BattleDefeat()
    {
        StopCoroutine(_battleCoroutine);
        _isPlayerTurn = false;
        _isBattleStart = false;

        SetInfoText($"{_playerInfo.Name}Àº(´Â) ÆÐ¹èÂg´Ù.\n{_playerInfo.Name}Àº(´Â) ´« ¾ÕÀÌ ±ô±ôÇØÁ³´Ù.");
        yield return new WaitForSeconds(0.5f);

        _gameInfo.wildPokemon = null;
        _playerInfo.PokemonList[0] = _playerPokemon; // °æÇèÄ¡°¡ ¾È³Ñ¾î °£´Ù.
        _gameInfo.PlayerInfo = _playerInfo;
        Managers.Save.SaveJson(_gameInfo);

        Managers.Scene.LoadScene(Define.Scene.Map);
    }

    public void Attack(SkillSO skill)
    {
        // ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½ ï¿½ï¿½
        int rand = Random.Range(0, 101);
        if (rand <= skill.accuracyRate)
        {
            bool isCritical = Random.value <= 0.06f ? true : false;
            DamageType type = _enemyPokemon.Damage(skill.power, _playerPokemon.Attack, skill.type, isCritical);

            SetInfoText($"{PlayerPokemon.Name}ï¿½ï¿½ {skill.skillName}!");
            StartCoroutine(ChangeTurnCoroutine(type));
            UpdateUI();
            AllClosePanel();
        }
        else
        {
            SetInfoText($"{PlayerPokemon.Name}ï¿½ï¿½ {skill.skillName}ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½!");
        }
    }

    private IEnumerator ChangeTurnCoroutine(DamageType type)
    {
        switch (type)
        {
            case DamageType.GREAT:
                yield return new WaitForSeconds(0.5f);
                SetInfoText("È¿°ú°¡ ±²ÀåÇß´Ù!");
                break;
            case DamageType.MEDIOCRE:
                break;
            case DamageType.NOTGOOD:
                yield return new WaitForSeconds(0.5f);
                SetInfoText("È¿°ú°¡ º°·Î´Ù.");
                break;
            case DamageType.NO:
                yield return new WaitForSeconds(0.5f);
                SetInfoText("È¿°ú°¡ ¾ø´Ù.");
                break;
        }

        if (_enemyPokemon.Hp <= 0)
        {
            int exp = Mathf.Max((_enemyPokemon.Level - _playerPokemon.Level) * (_playerPokemon.Level / 2), 1) + 5;
            _playerPokemon.AddExp(exp);
            UpdateUI();

            StartCoroutine(BattleVictory());
            yield break;
            // ï¿½ï¿½ï¿½ï¿½Ä¡ ï¿½ï¿½ï¿½ï¿½?
            // MAX((B2 - A2) * (A2 / 2), 1) + 5
        }
        else if(_playerPokemon.Hp <= 0)
        {
            // ¸¸¾à Æ÷ÄÏ¸óÀÌ ´õ ¾ø´Ù¸é
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

            if(idx != -1) // Æ÷ÄÏ¸óÀÌ ÀÖ´Ù
            {
                SetInfoText($"¼ö°íÇß¾î {_playerPokemon.Name}");
                SwapPokemon(0, idx);
                SetInfoText($"°¡¶ù! {_playerPokemon.Name}!");
                SetTurn();
            }
            else // Æ÷ÄÏ¸óÀÌ ¾ø´Ù.
            {
                StartCoroutine(BattleDefeat());
            }
            yield break;
            // ¸¸¾à Æ÷ÄÏ¸óÀÌ ÀÖ´Ù¸é
            // SwapPokemon(0, 1); // 1À» ¾ÈÁ×Àº °¡Àå ÀÛÀº ÀÎµ¦½º¸¦ °¡Áø Æ÷ÄÏ¸óÀ¸·Î ±³Ã¼
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

        // ï¿½ï¿½Ã¼ ï¿½ï¿½ï¿½ï¿½Æ®

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
        SetInfoText("¹«»çÈ÷ µµ¸ÁÃÆ´Ù.");
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
