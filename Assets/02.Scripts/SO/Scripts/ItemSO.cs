using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Creature/Pokemon/Item"), System.Serializable]
public class ItemSO : ScriptableObject
{
    public new string name;
    public string description;
    [ShowAssetPreview(64, 64)]
    public Sprite image;
}
