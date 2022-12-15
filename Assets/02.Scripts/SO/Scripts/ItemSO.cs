using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Creature/Pokemon/Item")]
public class ItemSO : ScriptableObject
{
    public new string name;
    public string description;
    public Image image;
}
