using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStract
{
    public string name;
    public int level;
    public int maxHp;
    public int curHp;
    public Slider hpBar;
    public Slider levelBar;

    public UIStract(string name, int level, int maxHp, int curHp, Slider hpBar, Slider levelBar)
    {
        this.name = name;
        this.level = level;
        this.maxHp = maxHp;
        this.curHp = curHp;
        this.hpBar = hpBar;
        this.levelBar = levelBar;
    }
}

public class BattleScene : BaseScene
{
    [SerializeField]
    private GameObject _playerInfoPanel;
    [SerializeField]
    private GameObject _enemyInfoPanel;

    private UIStract _playerUIStract;
    private UIStract _enemyUIStract;

    private BattleInfo enemyInfo;

    private PokeInformationSO _playerPokemon;
    private PokeInformationSO _enemyPokemon;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Battle;

        enemyInfo = Managers.Save.LoadJsonFile<BattleInfo>();
        
        // 이거 싹다 갈아 엎어야함

        //foreach (var info in enemyInfo.enemyPokemonList)
        //{
        //    Debug.Log($"이름:{info.name}, 레벨:{info.Level}, 공격력:{info.CurrentAttack}, 방어력:{info.CurrentDefense}, 체력:{info.CurrentHP}");
        //}

        //_enemyPokemon = enemyInfo.enemyPokemonList[0];

        _playerUIStract = new UIStract(_enemyPokemon.name, _enemyPokemon.Level, (int)_enemyPokemon.PokeHP, _enemyPokemon.CurrentHP, _enemyInfoPanel.transform.Find("HP Bar").GetComponent<Slider>(), _enemyInfoPanel.transform.Find("Exp Bar").GetComponent<Slider>());

        // TODO : 1. UI에 정보 연결
        // TODO : 2. UI Amination
    }

    public void SetUI(UIStract ui, bool isEnemy)
    {
        Slider hpBar;
        Slider levelBar;
        Text nameText;
        Text levelText;
        Text hpText;

        if(isEnemy == true)
        {
            //_enemyInfoPanel
        }
        else
        {
            //_playerInfoPanel;
        }
    }

    public override void Clear()
    {
        
    }
}
