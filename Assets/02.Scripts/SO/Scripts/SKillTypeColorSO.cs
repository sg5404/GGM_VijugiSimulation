using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypePair
{
    public Define.PokeType type;
    public Color color;
}

[CreateAssetMenu(menuName = "SO/Creature/Pokemon/Skill/Color")]
public class SKillTypeColorSO : ScriptableObject
{
    public List<TypePair> colorList;
}
