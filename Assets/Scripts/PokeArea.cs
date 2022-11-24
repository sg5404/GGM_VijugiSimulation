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
        Debug.Log($"이름:{poke[pokeNum].name}, 레벨:{poke[pokeNum].Level}, 공격력:{poke[pokeNum].CurrentAttack}, 방어력:{poke[pokeNum].CurrentDefense}, 체력:{poke[pokeNum].CurrentHP}");
    }

    void SetPokeMon() //나중에 레어리티에 따라서 나오는 확률 다르게 해줘야함
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
