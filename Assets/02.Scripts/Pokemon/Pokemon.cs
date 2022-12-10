using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Pokemon : MonoBehaviour
{
    [SerializeField]
    protected PokemonInfoSO _HABCDS; // Á¾Á·°ª

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

    protected Define.PokeType _mainType;
    protected Define.PokeType _subType;

    protected Define.PokeRarity _rarity;

    protected string _name;
}
