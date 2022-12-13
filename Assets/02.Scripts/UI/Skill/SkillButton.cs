using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Text _skillName;
    [SerializeField] private Text _skillTypeText;
    [SerializeField] private Image _skillTypeImage;

    public void SetInfo(string name, Define.PokeType type)
    {
        SetInfo(name, Enum.GetName(typeof(Define.PokeType), type));
    }

    public void SetInfo(string name, string type)
    {
        _skillName.text = name;
        _skillTypeText.text = type;
    }
}
