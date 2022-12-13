using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PokeArea : MonoBehaviour
{
    [SerializeField] private int pokePercent;
    [SerializeField] private List<PokeInformationSO> poke;
    [SerializeField] private List<PokemonInfoSO> pokemonList;
    [SerializeField, MinValue(1), MaxValue(100)] private int minLevel;
    [SerializeField, MinValue(1), MaxValue(100)] private int maxLevel;

    private int currentLevel;
    private int pokeNum;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerControl control = other.GetComponent<PlayerControl>();
        Player player = other.GetComponent<Player>();

        if (control == null) return;
        if (control.h + control.v < 0.1f) return;
        control.timer += Time.deltaTime;
        if (control.timer < 1f) return;
        control.timer = 0;
        if (Random.Range(0, 100.0f) > pokePercent) return;
        SetPokeMon();
        Pokemon wildPokemon = new Pokemon(pokemonList[pokeNum], SetLevel());
        GameInfo info = new GameInfo();
        info.PlayerInfo = player.GetInfo();
        info.isWildPokemon = true;
        info.wildPokemon = wildPokemon;
        Managers.Save.SaveJson(info);
        Managers.Scene.LoadScene(Define.Scene.Battle);
    }

    void SetPokeMon() //나중에 레어리티에 따라서 나오는 확률 다르게 해줘야함
    {
        //pokeNum = Random.Range(0, poke.Count);
        pokeNum = Random.Range(0, pokemonList.Count);
    }

    //void SetLevel()
    //{
    //    currentLevel = Random.Range(minLevel, maxLevel + 1);
    //    //poke[pokeNum].Level = currentLevel;
    //}

    int SetLevel()
    {
        return Random.Range(minLevel, maxLevel + 1);
    }

    void LevelStats()
    {
        var pokeMon = poke[pokeNum];
        int basicStat = pokeMon.Rarity switch
        {
            Define.PokeRarity.Common => 10,
            Define.PokeRarity.Rare => 15,
            Define.PokeRarity.unique => 20,
            Define.PokeRarity.Legendary => 30,
            _ => 10,
        };

        pokeMon.CurrentAttack = basicStat + (int)(pokeMon.Level * pokeMon.PokeAttack);
        pokeMon.CurrentDefense = basicStat + (int)(pokeMon.Level * pokeMon.PokeDefense);
        pokeMon.CurrentHP = basicStat + (int)(pokeMon.Level * pokeMon.PokeHP);
        pokeMon.CurrentSpeed = basicStat + (int)(pokeMon.Level * pokeMon.PokeSpeed);
    }
}
