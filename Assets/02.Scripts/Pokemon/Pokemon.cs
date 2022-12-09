using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Pokemon : MonoBehaviour
{
    private int _curHp;
    private int _maxHp;

    private int _level;
    private int _curExp;
    private int _maxExp;

    private int _attack;
    private int _defence;
    private int _speed;

    private Skill[] _skillList = new Skill[4];

    public Pokemon(int curHp, int maxHp, int level, int curExp, int maxExp, int attack, int defence, int speed, Skill[] skillList)
    {
        _curHp = curHp;
        _maxHp = maxHp;
        _level = level;
        _curExp = curExp;
        _maxExp = maxExp;
        _attack = attack;
        _defence = defence;
        _speed = speed;
        _skillList = skillList;
    }

    public Pokemon(PokeInformationSO info)
    {
        _curHp = info.CurrentHP;
        _maxHp = info.CurrentHP;
        _level = info.Level;
        _curExp = 0;
        _maxExp = 100;
        _attack = info.CurrentAttack;
        _defence = info.CurrentDefense;
        _speed = info.CurrentSpeed;
        //for(int k)
    }
}
