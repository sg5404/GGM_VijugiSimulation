using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    private const int MAX_SKILL_COUNT = 4;

    private SkillSO[] _skillList = new SkillSO[MAX_SKILL_COUNT];

    private SkillButton[] _skillBtnList = new SkillButton[MAX_SKILL_COUNT];

    private void OnEnable()
    {
        for (int i = 0; i < MAX_SKILL_COUNT; i++)
        {
            _skillBtnList[i] = transform.GetChild(i).GetComponent<SkillButton>();
        }
    }

    public void SetSkill(SkillSO[] skillList)
    {
        for(int i = 0; i < _skillBtnList.Length; i++)
        {
            _skillList[i] = skillList[i];
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        SetAllBtnActive();
    }

    private void SetAllBtnActive()
    {
        for (int i = 0; i < MAX_SKILL_COUNT; i++)
        {
            //if (_skillList[i] == null)
            //{
            //    _skillBtnList[i].gameObject.SetActive(false);
            //}
            //else
            //{
            //    _skillBtnList[i].gameObject.SetActive(true);
            //    UpdateBtnInfoOfIndex(i);
            //}

            _skillBtnList[i].gameObject.SetActive(true);
            UpdateBtnInfoOfIndex(i);
        }
    }

    private void UpdateBtnInfoOfIndex(int index)
    {
        if (_skillList[index] != null)
        {
            _skillBtnList[index].SetInfo(_skillList[index].name, _skillList[index].type);
        }
        else
        {
            _skillBtnList[index].SetInfo("", "");
        }
    }
}
