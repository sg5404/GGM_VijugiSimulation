using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Text _skillName;
    [SerializeField] private Text _skillTypeText;
    [SerializeField] private Image _skillTypeImage;

    [SerializeField] private SKillTypeColorSO _skillColor;

    private Button _btn;

    private SkillSO _skill;

    private void OnEnable()
    {
        _btn = GetComponent<Button>();

        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(() => Attack());
    }

    private void Attack()
    {
        if (_skill == null) return;

        BattleScene scene = Managers.Scene.CurrentScene as BattleScene;
        if (scene == null) return;
        if (scene.IsPlayerTurn == false) return;

        scene.Attack(_skill);
    }

    

    private void SetInfo(string name, Define.PokeType type)
    {
        if (name == "" || name == null || type == Define.PokeType.None)
        {
            _skillName.text = "";
            _skillTypeText.text = "";
            _skillTypeImage.gameObject.SetActive(false);
        }
        else
        {
            _skillTypeImage.gameObject.SetActive(true);
            _skillTypeImage.color = GetColor(type);
            _skillName.text = name;
            _skillTypeText.text = Enum.GetName(typeof(Define.PokeType), type);
        }
    }

    public void SetSkill(SkillSO skill)
    {
        _skill = skill;

        if (skill != null)
        {
            SetInfo(_skill.skillName, _skill.type);
        }
        else
        {
            SetInfo("", Define.PokeType.None);
        }
    }

    private Color GetColor(Define.PokeType type)
    {
        foreach (var color in _skillColor.colorList)
        {
            if (color.type == type)
                return color.color;
        }

        return Color.black;
    }

    public void AddEvent(UnityAction action)
    {
        _btn.onClick.AddListener(action);
    }
}
