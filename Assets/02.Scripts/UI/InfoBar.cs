using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class UIInfo
{
    public string name;
    public int level;
    public int hp;
    public int maxHp;
    public int exp;
    public int maxExp;
}

public class InfoBar : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _hpText;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _expBar;

    private int _hp;
    private int _maxHp;

    private int _exp;
    private int _maxExp;

    public void SetInfo(string name, int level, int hp, int maxHp, int exp, int maxExp)
    {
        _nameText.text = name;
        _levelText.text = "Lv. " + level.ToString();
        _hpText.text = $"{hp} / {maxHp}";

        _hp = hp;
        _maxHp = maxHp;
        _hpBar.value = (float)hp / maxHp;
        if(hp <= 0)
        {
            _hpBar.transform.Find("Fill Area").gameObject.SetActive(false);
        }
        else
        {
            _hpBar.transform.Find("Fill Area").gameObject.SetActive(true);
        }

        _exp = exp;
        _maxExp = maxExp;
        _expBar.value = (float)exp / maxExp;
        if(exp <= 0)
        {
            _expBar.transform.Find("Fill Area").gameObject.SetActive(false);
        }
        else
        {
            _expBar.transform.Find("Fill Area").gameObject.SetActive(true);
        }
    }

    public void SetInfo(UIInfo info)
    {
        SetInfo(info.name, info.level, info.hp, info.maxHp, info.exp, info.maxExp);
    }

    public void SetHpBar(int hp)
    {
        _hpBar.value = (float)hp / _maxHp;
    }

    public void SetExpBar(int exp)
    {
        _expBar.value = (float)exp / _maxExp;
    }

    public void SetActiveExpBar(bool value)
    {
        _expBar.gameObject.SetActive(value);
    }


    public void SetActiveHpText(bool value)
    {
        _hpText.gameObject.SetActive(value);
    }
}
