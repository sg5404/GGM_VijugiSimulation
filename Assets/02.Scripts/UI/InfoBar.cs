using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour
{
    private Text _nameText;
    private Text _levelText;
    private Text _hpText;
    private Slider _hpBar;
    private Slider _expBar;

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
}
