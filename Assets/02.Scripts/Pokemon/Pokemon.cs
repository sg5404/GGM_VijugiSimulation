using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.LudiqRootObjectEditor;

public enum AbilityType
{
    NONE,
    HP,
    ATTACK,
    BLOCK,
    SPEED,
}

public class Pokemon : MonoBehaviour
{
    [SerializeField]
    protected PokemonInfoSO _info;

    // 0 ~ 31까지
    protected int _individualValue = 0; // 개체값

    protected int _hp;
    protected int _attack;
    protected int _block;
    protected int _speed;

    private const int MAX_SKILL_CNT = 4;
    protected Skill[] _skillList = new Skill[MAX_SKILL_CNT];

    protected int _level;

    protected int _curExp;
    protected int _maxExp;
    protected int _accExp;

    protected string _name;

    //[{(종족값 * 2) + 개체값} * 레벨/100] + 10 + 레벨

    #region Private Method
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
        return (int)((((value * 2) + _individualValue) * _level / 100) + 10 + _level);
    }

    private int AbilityFormula(int value)
    {
        return (int)((((value * 2) + _individualValue) * _level / 100) + 10 + _level);
    }

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
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Fighting:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Poison:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Ground:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Flying:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Bug:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Rock:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Ghost:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Steel:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Fire:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Water:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Electric:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Grass:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Ice:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Psychic:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Dragon:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Dark:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
            case Define.PokeType.Fairy:
                switch (thisType)
                {
                    case Define.PokeType.None:
                        break;
                    case Define.PokeType.Normal:
                        break;
                    case Define.PokeType.Fighting:
                        break;
                    case Define.PokeType.Poison:
                        break;
                    case Define.PokeType.Ground:
                        break;
                    case Define.PokeType.Flying:
                        break;
                    case Define.PokeType.Bug:
                        break;
                    case Define.PokeType.Rock:
                        break;
                    case Define.PokeType.Ghost:
                        break;
                    case Define.PokeType.Steel:
                        break;
                    case Define.PokeType.Fire:
                        break;
                    case Define.PokeType.Water:
                        break;
                    case Define.PokeType.Electric:
                        break;
                    case Define.PokeType.Grass:
                        break;
                    case Define.PokeType.Ice:
                        break;
                    case Define.PokeType.Psychic:
                        break;
                    case Define.PokeType.Dragon:
                        break;
                    case Define.PokeType.Dark:
                        break;
                    case Define.PokeType.Fairy:
                        break;
                }
                break;
        }
    }

    private float TypeCompatibility(Define.PokeType type)
    {
        return GetValue(_info.mainType, type) * GetValue(_info.subType, type);
    }
    #endregion

    #region Public Method
    public void SetInfo()
    {
        _hp = GetAbilityValue(AbilityType.HP);
        _attack = GetAbilityValue(AbilityType.ATTACK);
        _block = GetAbilityValue(AbilityType.BLOCK);
        _speed = GetAbilityValue(AbilityType.SPEED);
    }

    public void Damage(float amount, Define.PokeType type, bool isCritical = false)
    {
        // (((((레벨 × 2 ÷ 5) + 2) × 위력 × 특수공격 ÷ 50) ÷ 특수방어) + 2) * 급소* 상성1 * 상성2
        // amount = 위력 * 특수공격
        float typeCom = TypeCompatibility(type);
        int damage = (int)(((((((float)_level * 2 / 5) + 2) * amount / 50) / (float)_block) + 2) * (isCritical ? 2 : 1) * typeCom);
        this._hp -= damage;

        // Update UI
    }
    #endregion
}
