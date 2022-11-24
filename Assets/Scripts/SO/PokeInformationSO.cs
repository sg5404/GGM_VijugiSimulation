using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PokeInformationSO", menuName = "SO/PokeMon")]
public class PokeInformationSO : ScriptableObject
{
    [SerializeField] private string pokeName; //이름
    [SerializeField] private Image pokeImage;
    [SerializeField] private Image pokeBackImage;

    public string PokeName { get { return pokeName; } }

    public int Level; //레벨
    public PokeRarity Rarity; //희귀도
    public PokeType MainType; //주속성
    public PokeType SubType; //부속성
    public int CurrentAttack; //현재 공격력
    public int CurrentDefense; //현재 방어력
    public int CurrentHP; //현재 체력

    [SerializeField] private float pokeAttack; //레벨당 공격력
    public float PokeAttack { get { return pokeAttack; } }

    [SerializeField] private float PokeHP; //레벨당 체력
    public float pokeHP { get { return PokeHP; } }

    [SerializeField] private float pokeDefense; //레벨당 방어력
    public float PokeDefense { get { return pokeDefense; } }
}
