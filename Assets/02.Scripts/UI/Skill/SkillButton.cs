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
         
        bool isCritical = Random.value <= 0.06f ? true : false;
        DamageType type = scene.EnemyPokemon.Damage(_skill.power, scene.PlayerPokemon.Attack, _skill.type, isCritical);

        scene.SetInfoText($"{scene.PlayerPokemon.Name}의 {_skill.skillName}!");
        StartCoroutine(ChangeTurn(scene, type));
        scene.UpdateUI();
        scene.AllClosePanel();
    }

    private IEnumerator ChangeTurn(BattleScene scene, DamageType type)
    {
        yield return new WaitForSeconds(0.5f);
        switch (type)
        {
            case DamageType.GREAT:
                scene.SetInfoText("효과가 굉장했다.");
                break;
            case DamageType.MEDIOCRE:
                break;
            case DamageType.NOTGOOD:
                scene.SetInfoText("효과가 별로다.");
                break;
            case DamageType.NO:
                scene.SetInfoText("효과가 없다.");
                break;
        }
        scene.ChangeTurn();
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
