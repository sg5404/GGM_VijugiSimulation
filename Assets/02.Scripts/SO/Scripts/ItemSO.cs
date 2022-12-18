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
    [ShowAssetPreview(32, 32)]
    public Sprite image;
}
