using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Text _skillName;
    [SerializeField] private Text _skillTypeText;
    [SerializeField] private Image _skillTypeImage;

    private Button _btn;

    private void Start()
    {
        _btn= GetComponent<Button>();
    }

    public void SetInfo(string name, Define.PokeType type)
    {
        SetInfo(name, Enum.GetName(typeof(Define.PokeType), type));
    }

    public void SetInfo(string name, string type)
    {
        if (name == "" || type == "" || type == null || name == null)
        {
            _skillName.text = "";
            _skillTypeText.text = "";
            _skillTypeImage.gameObject.SetActive(false);
        }
        else
        {
            _skillTypeImage.gameObject.SetActive(true);
            _skillName.text = name;
            _skillTypeText.text = type;
        }
    }

    public void AddEvent(UnityAction action)
    {
        _btn.onClick.AddListener(action);
    }
}
