using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillPair
{
    //public 
}

public class PokemonInfoSO : MonoBehaviour
{
    public PokemonHABCDSSO habcds; // Á¾Á·°ª

    public new string name;
    public Define.PokeRarity rarity;
    public Define.PokeType mainType;
    public Define.PokeType subType;

    public List<SkillPair> skillTree;
}
