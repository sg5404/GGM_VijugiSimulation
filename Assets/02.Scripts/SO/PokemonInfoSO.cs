using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;



[CreateAssetMenu(menuName = "SO/Creature/Pokemon/Info")]
public class PokemonInfoSO : ScriptableObject
{
    public PokemonHABCDSSO habcds; // ������

    public new string name;
    [ShowAssetPreview(32, 32)]
    public Sprite image;
    public Define.PokeRarity rarity;
    public Define.PokeType mainType;
    public Define.PokeType subType;

    public SkillTreeSO skillTree; // ��ų Ʈ��
}
