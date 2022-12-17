using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum AbilityType
{
    NONE,
    HP,
    ATTACK,
    BLOCK,
    SPEED,
}

[System.Serializable]
public class Pokemon
{
    [SerializeField]
    protected PokemonInfoSO _info;
    public PokemonInfoSO Info => _info;

    [SerializeField]
    // 0 ~ 31����
    protected int _individualValue = 0; // ��ü��

    [SerializeField] protected int _hp;
    [SerializeField] protected int _maxHp;
    public int Hp => _hp;
    public int MaxHp => _maxHp;
    [SerializeField] protected int _attack;
    public int Attack => _attack;
    [SerializeField] protected int _block;
    public int Block => _block;
    [SerializeField] protected int _speed;
    public int Speed => _speed;

    private const int MAX_SKILL_CNT = 4;
    [SerializeField] protected SkillSO[] _skillList = new SkillSO[MAX_SKILL_CNT];
    public SkillSO[] SkillList => _skillList;

    [SerializeField] protected int _level;
    public int Level => _level;

    [SerializeField] protected int _curExp; // ���� ����ġ ��
    public int CurExp => _curExp;
    [SerializeField] protected int _maxExp; // �������� ���� ������� ����ġ ��
    public int MaxExp => _maxExp;
    [SerializeField] protected int _CurAccExp; // ���� ���� ���� ����ġ �� (���� ��������� ���� ����ġ�� �ƴԡ�)
    [SerializeField] protected int _befAccExp; // ���� ���� ���� ����ġ ��
    [SerializeField] protected int _NexAccExp; // ���� ���� ���� ����ġ ��

    [SerializeField] protected string _name;
    public string Name => _name;



    //[{(������ * 2) + ��ü��} * ����/100] + 10 + ����

    public Pokemon()
    {

    }

    public Pokemon(PokemonInfoSO info, int level)
    {
        _info = info;
        _level = level;
        _individualValue = SetIV();
        SetPokemonInfo();
        _hp = _maxHp;

        _name = _info.name;

        int beforeLevel = _level - 1;
        _befAccExp = (beforeLevel * beforeLevel * beforeLevel);
        _curExp = _befAccExp;
        _CurAccExp = (_level * _level * _level);
        int nextLevel = _level + 1;
        _NexAccExp = (nextLevel * nextLevel * nextLevel);
        _maxExp = _NexAccExp - _CurAccExp;

        // ��ų ������ ���� ��� ó��
    }

    public Pokemon(Pokemon pokemon) 
    {
        _info = pokemon.Info;
        _level = pokemon.Level;
        _individualValue = pokemon._individualValue;
        SetPokemonInfo();
        _maxHp = pokemon._maxHp;

        _name = _info.name;

        _curExp = pokemon.CurExp;
        _befAccExp = pokemon._befAccExp;
        _CurAccExp = pokemon._CurAccExp;
        _NexAccExp = pokemon._NexAccExp;
        _maxExp = _NexAccExp - _CurAccExp;

        // ��ų ������ ���� ��� ó��
    }

    #region Private Method
    private int SetIV()
    {
        return Random.Range(0, 32);
    }

    private int GetAbilityValue(AbilityType type)
    {
        return type switch
        {
            AbilityType.NONE => 0,
            AbilityType.HP => AbilityFormula(_info.habcds.hp),
            AbilityType.ATTACK => AbilityFormula(_info.habcds.attack),
            AbilityType.BLOCK => AbilityFormula(_info.habcds.block),
            AbilityType.SPEED => AbilityFormula(_info.habcds.speed),
            _ => 0,
        };
    }

    private int AbilityFormula(float value)
    {
        return Mathf.FloorToInt(((((value * 2) + (float)_individualValue) * (float)_level / 100) + 10 + (float)_level));
    }

    private int AbilityFormula(int value)
    {
        return AbilityFormula((float)value);
    }

    // thisType: ���ݹ޴� ��, skillType: �����ϴ� ��
    private float GetValue(Define.PokeType thisType, Define.PokeType skillType) 
    {
        switch (skillType)
        {
            case Define.PokeType.None:
                return 1;
            case Define.PokeType.Normal:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 1;   
                    case Define.PokeType.Rock:
                        return 0.5f;
                    case Define.PokeType.Ghost:
                        return 0;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 1;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 1;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Fighting:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 2;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 0.5f;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 0.5f;
                    case Define.PokeType.Bug:
                        return 0.5f;
                    case Define.PokeType.Rock:
                        return 2;
                    case Define.PokeType.Ghost:
                        return 0;
                    case Define.PokeType.Steel:
                        return 2;
                    case Define.PokeType.Fire:
                        return 1;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 1;
                    case Define.PokeType.Ice:
                        return 2;
                    case Define.PokeType.Psychic:
                        return 0.5f;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 2;
                    case Define.PokeType.Fairy:
                        return 0.5f;
                }
                break;
            case Define.PokeType.Poison:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 0.5f;
                    case Define.PokeType.Ground:
                        return 0.5f;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 0.5f;
                    case Define.PokeType.Ghost:
                        return 0.5f;
                    case Define.PokeType.Steel:
                        return 0;
                    case Define.PokeType.Fire:
                        return 1;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 2;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 2;
                }
                break;
            case Define.PokeType.Ground:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 2;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 0;
                    case Define.PokeType.Bug:
                        return 0.5f;
                    case Define.PokeType.Rock:
                        return 2;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 2;
                    case Define.PokeType.Fire:
                        return 2;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 2;
                    case Define.PokeType.Grass:
                        return 0.5f;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Flying:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 2;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 2;
                    case Define.PokeType.Rock:
                        return 0.5f;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 1;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 0.5f;
                    case Define.PokeType.Grass:
                        return 2;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Bug:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 0.5f;
                    case Define.PokeType.Poison:
                        return 0.5f;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 0.5f;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 1;
                    case Define.PokeType.Ghost:
                        return 0.5f;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 0.5f;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 2;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 2;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 2;
                    case Define.PokeType.Fairy:
                        return 0.5f;
                }
                break;
            case Define.PokeType.Rock:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 0.5f;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 0.5f;
                    case Define.PokeType.Flying:
                        return 2;
                    case Define.PokeType.Bug:
                        return 2;
                    case Define.PokeType.Rock:
                        return 1;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 2;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 1;
                    case Define.PokeType.Ice:
                        return 2;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Ghost:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 0;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 1;
                    case Define.PokeType.Ghost:
                        return 2;
                    case Define.PokeType.Steel:
                        return 1;
                    case Define.PokeType.Fire:
                        return 1;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 1;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 2;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 0.5f;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Steel:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 2;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 0.5f;
                    case Define.PokeType.Water:
                        return 0.5f;
                    case Define.PokeType.Electric:
                        return 0.5f;
                    case Define.PokeType.Grass:
                        return 1;
                    case Define.PokeType.Ice:
                        return 2;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 2;
                }
                break;
            case Define.PokeType.Fire:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 2;
                    case Define.PokeType.Rock:
                        return 0.5f;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 2;
                    case Define.PokeType.Fire:
                        return 0.5f;
                    case Define.PokeType.Water:
                        return 0.5f;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 2;
                    case Define.PokeType.Ice:
                        return 2;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 0.5f;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Water:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 2;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 2;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 1;
                    case Define.PokeType.Fire:
                        return 2;
                    case Define.PokeType.Water:
                        return 0.5f;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 0.5f;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 0.5f;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Electric:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 0;
                    case Define.PokeType.Flying:
                        return 2;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 1;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 1;
                    case Define.PokeType.Fire:
                        return 1;
                    case Define.PokeType.Water:
                        return 2;
                    case Define.PokeType.Electric:
                        return 0.5f;
                    case Define.PokeType.Grass:
                        return 0.5f;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 0.5f;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Grass:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 0.5f;
                    case Define.PokeType.Ground:
                        return 2;
                    case Define.PokeType.Flying:
                        return 0.5f;
                    case Define.PokeType.Bug:
                        return 0.5f;
                    case Define.PokeType.Rock:
                        return 2;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 0.5f;
                    case Define.PokeType.Water:
                        return 2;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 0.5f;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 0.5f;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Ice:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 2;
                    case Define.PokeType.Flying:
                        return 2;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 1;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 0.5f;
                    case Define.PokeType.Water:
                        return 0.5f;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 2;
                    case Define.PokeType.Ice:
                        return 0.5f;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 2;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Psychic:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 2;
                    case Define.PokeType.Poison:
                        return 2;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 1;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 1;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 1;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 0.5f;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 0;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
            case Define.PokeType.Dragon:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 1;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 1;  
                    case Define.PokeType.Rock:
                        return 1;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 1;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 1;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 2;
                    case Define.PokeType.Dark:
                        return 1;
                    case Define.PokeType.Fairy:
                        return 0;
                }
                break;
            case Define.PokeType.Dark:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 0.5f;
                    case Define.PokeType.Poison:
                        return 1;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 1;
                    case Define.PokeType.Ghost:
                        return 2;
                    case Define.PokeType.Steel:
                        return 1;
                    case Define.PokeType.Fire:
                        return 1;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 1;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 2;
                    case Define.PokeType.Dragon:
                        return 1;
                    case Define.PokeType.Dark:
                        return 0.5f;
                    case Define.PokeType.Fairy:
                        return 0.5f;
                }
                break;
            case Define.PokeType.Fairy:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        return 1;
                    case Define.PokeType.Normal:
                        return 1;
                    case Define.PokeType.Fighting:
                        return 2;
                    case Define.PokeType.Poison:
                        return 0.5f;
                    case Define.PokeType.Ground:
                        return 1;
                    case Define.PokeType.Flying:
                        return 1;
                    case Define.PokeType.Bug:
                        return 1;
                    case Define.PokeType.Rock:
                        return 1;
                    case Define.PokeType.Ghost:
                        return 1;
                    case Define.PokeType.Steel:
                        return 0.5f;
                    case Define.PokeType.Fire:
                        return 0.5f;
                    case Define.PokeType.Water:
                        return 1;
                    case Define.PokeType.Electric:
                        return 1;
                    case Define.PokeType.Grass:
                        return 1;
                    case Define.PokeType.Ice:
                        return 1;
                    case Define.PokeType.Psychic:
                        return 1;
                    case Define.PokeType.Dragon:
                        return 2;
                    case Define.PokeType.Dark:
                        return 2;
                    case Define.PokeType.Fairy:
                        return 1;
                }
                break;
        }

        return 1;
    }

    private float TypeCompatibility(Define.PokeType type)
    {
        return GetValue(_info.mainType, type) * GetValue(_info.subType, type);
    }

    private void SetPokemonInfo()
    {
        _maxHp = GetAbilityValue(AbilityType.HP);
        _attack = GetAbilityValue(AbilityType.ATTACK);
        _block = GetAbilityValue(AbilityType.BLOCK);
        _speed = GetAbilityValue(AbilityType.SPEED);
    }

    private void LevelUp()
    {
        int exp = _curExp - _maxExp;
        _level++;
        _curExp = 0;
        _befAccExp = _CurAccExp;
        _CurAccExp = (_level * _level * _level);
        int nextLevel = _level + 1;
        _NexAccExp = (nextLevel * nextLevel * nextLevel);
        _maxExp = _NexAccExp - _CurAccExp;
        AddExp(exp);

        SetPokemonInfo();

        _info.evolutionTree.ForEach(e =>
        {
            if(e.level == _level)
            {
                Evolution(e.pokemonSO);
            }
        });

        // Update UI
    }

    private bool IsEquipSkill()
    {
        return true; // �̷��� ���� ���ְ���
    }

    private int GetEmptySkillIndex()
    {
        return 0; // �̷��� ���� ���ְ��� 2
    }

    private void Evolution(PokemonInfoSO pokemon)
    {
        this._info = pokemon;
        _name = _info.name;
        SetPokemonInfo();
    }

    private void SkillCheck()
    {
        // info SO���� SkillTree Ž���ϱ�
        foreach(var skill in _info.skillTree.pairs)
        {
            // ���⼭ �Ұ� �ϱ�
        }
    }

    #endregion

    #region Public Method
    public void Damage(float amount, Define.PokeType type, bool isCritical = false)
    {
        // (((((���� �� 2 �� 5) + 2) �� ���� �� Ư������ �� 50) �� Ư�����) + 2) * �޼�* ��1 * ��2 * �ڼ� ����
        // amount = ���� * Ư������
        float typeCom = TypeCompatibility(type);
        bool mfx = (type == _info.mainType) || (type == _info.subType);
        int damage = (int)(((((((float)_level * 2f / 5f) + 2f) * amount / 50f) / (float)_block) + 2f) * (isCritical ? 2f : 1f) * typeCom * (mfx ? 2f : 1f));
        damage = Mathf.Max(damage, 1);
        this._hp -= damage;
    }

    public void Damage(float power, float attack, Define.PokeType type, bool isCritical = false)
    {
        // (((((���� �� 2 �� 5) + 2) �� ���� �� Ư������ �� 50) �� Ư�����) + 2) * �޼�* ��1 * ��2 * �ڼ� ����
        Damage(power * attack, type, isCritical);
    }

    public void Heal(int heal)
    {
        this._hp += heal;
        this._hp = Mathf.Min(_hp, _maxHp);

        // �̰� �ٸ������� �ű��
        BattleScene scene = Managers.Scene.CurrentScene as BattleScene;
        scene.UpdateUI();
    }

    public void AddExp(int exp)
    {
        if (_level < 100)
            _curExp += exp;

        if(_curExp >= _maxExp && _level < 100)
        {
            LevelUp();
        }
    }

    public void SetSkill(SkillSO skill)
    {
        if (IsEquipSkill() == true)
        {
            _skillList[GetEmptySkillIndex()] = skill;
        }
        else
        {
            // ��ų ��ﲨ�� â �ٿ�� ���ߵ� ����?
        }
    }

    public void SetSkill(SkillSO[] skillList)
    {
        for(int i = 0; i < MAX_SKILL_CNT; i++)
        {
            _skillList[i] = skillList[i];
        }
    }
    #endregion
}
