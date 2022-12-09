using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PokeInformationSO", menuName = "SO/Creature/PokeMon")]
public class PokeInformationSO : ScriptableObject
{
    [SerializeField] private string pokeName; //이름
    [SerializeField] private Image pokeImage;
    [SerializeField] private Image pokeBackImage;

    public string PokeName { get { return pokeName; } }

    public int Level; //레벨
    public Define.PokeRarity Rarity; //희귀도
    public Define.PokeType MainType; //주속성
    public Define.PokeType SubType; //부속성
    public int CurrentAttack; //현재 공격력
    public int CurrentDefense; //현재 방어력
    public int CurrentHP; //현재 체력
    public int CurrentSpeed; // 현재 스피드

    [SerializeField] private float pokeAttack; //레벨당 공격력
    public float PokeAttack { get { return pokeAttack; } }

    [SerializeField] private float pokeHP; //레벨당 체력
    public float PokeHP { get { return pokeHP; } }

    [SerializeField] private float pokeDefense; //레벨당 방어력
    public float PokeDefense { get { return pokeDefense; } }

    [SerializeField] private float pokeSpeed; // 레벨당 스피드
    public float PokeSpeed { get { return pokeSpeed; } }

    [SerializeField]
    private List<Skill> skillList;
    public List<Skill> SkillList { get { return skillList; } }
    [SerializeField]
    private int skillCnt = 2;
    public int SkillCnt => skillCnt;
}
