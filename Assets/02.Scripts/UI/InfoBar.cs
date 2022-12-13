using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

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

    // 이정보 클래스든 구조체든 만들기
    public void SetInfo(string name, int level, int hp, int maxHp, int exp, int maxExp)
    {
        _nameText.text = name;
        _levelText.text = level.ToString();
        _hpText.text = $"{hp} / {maxHp}";

        _hp = hp;
        _maxHp = maxHp;
        _hpBar.value = (float)hp / maxHp;

        _exp = exp;
        _maxExp = maxExp;
        _expBar.value = (float)exp / maxExp;
    }

    public void SetInfo(UIInfo info)
    {
        _nameText.text = info.name;
        _levelText.text = "Lv. " + info.level.ToString();
        _hpText.text = $"{info.hp} / {info.maxHp}";

        _hp = info.hp;
        _maxHp = info.maxHp;
        _hpBar.value = (float)info.hp / info.maxHp;

        _exp = info.exp;
        _maxExp = info.maxExp;
        _expBar.value = (float)info.exp / info.maxExp;
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

    public void UpdateUI()
    {

    }
}
