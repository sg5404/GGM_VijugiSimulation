using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PokeArea : MonoBehaviour
{
    //[SerializeField] private int pokePercent;
    [SerializeField, MinValue(0), MaxValue(100)] private int pokePercent;
    [SerializeField] private List<PokemonInfoSO> pokemonList;
    [SerializeField, MinValue(1), MaxValue(100)] private int minLevel;
    [SerializeField, MinValue(1), MaxValue(100)] private int maxLevel;

    private int currentLevel;
    private int pokeNum;

    private float timer; // �̰ɷ� �ٲٱ�

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerControl control = other.GetComponent<PlayerControl>();
        Player player = other.GetComponent<Player>();

        if (control == null) return;
        if (control.h + control.v < 0.1f) return;
        timer += Time.deltaTime;
        if (timer < 1f) return;
        timer = 0;
        if (Random.Range(0, 100.0f) > pokePercent) return;

        Pokemon wildPokemon = new Pokemon(SetPokeMon(), SetLevel());
        GameInfo info = new GameInfo();
        info.PlayerInfo = player.GetInfo();
        info.isWildPokemon = true;
        info.wildPokemon = wildPokemon;
        Managers.Save.SaveJson(info);
        Managers.Scene.LoadScene(Define.Scene.Battle);
    }

    PokemonInfoSO SetPokeMon() //?�켓�?지??
    {
        //pokeNum = Random.Range(0, poke.Count);
        pokeNum = Random.Range(0, pokemonList.Count);
        return pokemonList[pokeNum];
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

    //void LevelStats()
    //{
    //    var pokeMon = poke[pokeNum];
    //    int basicStat = pokeMon.Rarity switch
    //    {
    //        Define.PokeRarity.Common => 10,
    //        Define.PokeRarity.Rare => 15,
    //        Define.PokeRarity.unique => 20,
    //        Define.PokeRarity.Legendary => 30,
    //        _ => 10,
    //    };

    //    pokeMon.CurrentAttack = basicStat + (int)(pokeMon.Level * pokeMon.PokeAttack);
    //    pokeMon.CurrentDefense = basicStat + (int)(pokeMon.Level * pokeMon.PokeDefense);
    //    pokeMon.CurrentHP = basicStat + (int)(pokeMon.Level * pokeMon.PokeHP);
    //    pokeMon.CurrentSpeed = basicStat + (int)(pokeMon.Level * pokeMon.PokeSpeed);
    //}
}
