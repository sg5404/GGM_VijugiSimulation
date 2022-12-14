using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[System.Serializable]
public class EvolutionInfo
{
    public int level;
    public PokemonInfoSO pokemonSO;
}

[CreateAssetMenu(menuName = "SO/Creature/Pokemon/Info"), System.Serializable]
public class PokemonInfoSO : ScriptableObject
{
    public PokemonHABCDSSO habcds; // ������

    public new string name;
    [ShowAssetPreview(64, 64)]
    public Sprite image;
    [ShowAssetPreview(64, 64)]
    public GameObject prefab;
    public Define.PokeRarity rarity;
    public Define.PokeType mainType;
    public Define.PokeType subType;
    public Define.PokeScale scale;

    public SkillTreeSO skillTree; // ��ų Ʈ��

    public List<EvolutionInfo> evolutionTree;
}
