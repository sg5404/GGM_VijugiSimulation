using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected PokemonInfoSO _info;
    public PokemonInfoSO Info => _info;

    // 0 ~ 31까지
    protected int _individualValue = 0; // 개체값

    protected int _hp;
    protected int _maxHp;
    public int Hp => _hp;
    protected int _attack;
    public int Attack => _attack;
    protected int _block;
    public int Block => _block;
    protected int _speed;
    public int Speed => _speed;

    private const int MAX_SKILL_CNT = 4;
    protected SkillSO[] _skillList = new SkillSO[MAX_SKILL_CNT];

    protected int _level;
    public int Level => _level;

    protected int _curExp; // 현재 경험치 량
    public int CurExp => _curExp;
    protected int _maxExp; // 레벨업을 위해 벌어야할 경험치 량
    public int MaxExp => _maxExp;
    protected int _CurAccExp; // 현재 레벨 누적 경험치 량 (주의 ※현재까지 얻은 경험치량 아님※)
    protected int _befAccExp; // 이전 레벨 누적 경험치 량
    protected int _NexAccExp; // 다음 레빌 누적 경험치 량

    protected string _name;
    public string Name => _name;

    //[{(종족값 * 2) + 개체값} * 레벨/100] + 10 + 레벨

    public Pokemon()
    {

    }

    public Pokemon(PokemonInfoSO info, int level)
    {
        _info = info;
        SetPokemonInfo();
        _maxHp = _hp;

        _level = level;
        _name = _info.name;
        _individualValue = SetIV();

        _curExp = 0;
        _befAccExp = 0;
        _CurAccExp = (_level * _level * _level);
        int nextLevel = _level + 1;
        _NexAccExp = (nextLevel * nextLevel * nextLevel);
        _maxExp = _NexAccExp - _curExp;
    }

    public Pokemon(Pokemon pokemon) // 복사 생성자. 이게 맞나...
    {
        _info = pokemon.Info;
        SetPokemonInfo();
        _maxHp = pokemon._maxHp;

        _level = pokemon.Level;
        _name = _info.name;
        _individualValue = pokemon._individualValue;

        _curExp = pokemon.CurExp;
        _befAccExp = pokemon._befAccExp;
        _CurAccExp = pokemon._CurAccExp;
        _NexAccExp = pokemon._NexAccExp;
        _maxExp = _NexAccExp;
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
        return (int)((((value * 2) + (float)_individualValue) * (float)_level / 100) + 10 + (float)_level);
    }

    private int AbilityFormula(int value)
    {
        return (int)(((((float)value * 2) + (float)_individualValue) * (float)_level / 100) + 10 + (float)_level);
    }

    // thisType: 공격받는 쪽, skillType: 공격하는 쪽
    private float GetValue(Define.PokeType thisType, Define.PokeType skillType) // 이거 개노가다하기
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
        _hp = GetAbilityValue(AbilityType.HP);
        _attack = GetAbilityValue(AbilityType.ATTACK);
        _block = GetAbilityValue(AbilityType.BLOCK);
        _speed = GetAbilityValue(AbilityType.SPEED);
    }

    private void LevelUp()
    {
        int exp = _curExp - _maxExp;
        _level++;
        _curExp = 0;
        _curExp += exp;
        _befAccExp = _curExp;
        _CurAccExp = (_level * _level * _level);
        int nextLevel = _level + 1;
        _NexAccExp = (nextLevel * nextLevel * nextLevel);
        _maxExp = _NexAccExp - _CurAccExp;

        SetPokemonInfo();

        // Update UI
    }
    #endregion

    #region Public Method

    public void Damage(float amount, Define.PokeType type, bool isCritical = false)
    {
        // (((((레벨 × 2 ÷ 5) + 2) × 위력 × 특수공격 ÷ 50) ÷ 특수방어) + 2) * 급소* 상성1 * 상성2
        // amount = 위력 * 특수공격
        float typeCom = TypeCompatibility(type);
        int damage = (int)(((((((float)_level * 2 / 5) + 2) * amount / 50) / (float)_block) + 2) * (isCritical ? 2 : 1) * typeCom);
        this._hp -= damage;

        // Update UI
    }

    public void Damage(float power, float attack, Define.PokeType type, bool isCritical = false)
    {
        // (((((레벨 × 2 ÷ 5) + 2) × 위력 × 특수공격 ÷ 50) ÷ 특수방어) + 2) * 급소* 상성1 * 상성2
        // amount = 위력 * 특수공격
        float typeCom = TypeCompatibility(type);
        int damage = (int)(((((((float)_level * 2 / 5) + 2) * power * attack / 50) / (float)_block) + 2) * (isCritical ? 2 : 1) * typeCom);
        this._hp -= damage;

        // Update UI
    }

    public void AddExp(int exp)
    {
        _curExp += exp;

        if(_curExp >= _maxExp)
        {
            LevelUp();
        }
    }
    #endregion
}
