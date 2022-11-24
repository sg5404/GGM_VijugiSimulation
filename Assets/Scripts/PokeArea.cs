using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeArea : MonoBehaviour
{
    [SerializeField] private int pokePercent;
    [SerializeField] private List<PokeInformationSO> poke;
    [SerializeField] private int minLevel;
    [SerializeField] private int maxLevel;

    private int currentLevel;
    private int pokeNum;

    private void OnTriggerStay(Collider other)
    {
        var control = PlayerControl.Instance;

        if (!other.CompareTag("Player")) return;
        if (control.h + control.v < 0.1f) return;
        control.timer += Time.deltaTime;
        if (control.timer < 1f) return;
        control.timer = 0;
        if (Random.Range(0, 100.0f) > 15) return;
        SetPokeMon();
        SetLevel();
        LevelStats();
        Debug.Log($"�̸�:{poke[pokeNum].name}, ����:{poke[pokeNum].Level}, ���ݷ�:{poke[pokeNum].CurrentAttack}, ����:{poke[pokeNum].CurrentDefense}, ü��:{poke[pokeNum].CurrentHP}");
    }

    void SetPokeMon() //���߿� ���Ƽ�� ���� ������ Ȯ�� �ٸ��� �������
    {
        pokeNum = Random.Range(0, poke.Count - 1);
    }

    void SetLevel()
    {
        currentLevel = Random.Range(minLevel, maxLevel + 1);
        poke[pokeNum].Level = currentLevel;
    }

    void LevelStats()
    {
        var pokeMon = poke[pokeNum];
        int basicStat = pokeMon.Rarity switch
        {
            PokeRarity.Common => 10,
            PokeRarity.Rare => 15,
            PokeRarity.unique => 20,
            PokeRarity.Legendary => 30,
            _ => 10,
        };

        pokeMon.CurrentAttack = basicStat + (int)(pokeMon.Level * pokeMon.PokeAttack);
        pokeMon.CurrentDefense = basicStat + (int)(pokeMon.Level * pokeMon.PokeDefense);
        pokeMon.CurrentHP = basicStat + (int)(pokeMon.Level * pokeMon.pokeHP);
    }
}
