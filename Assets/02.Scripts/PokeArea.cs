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
        if (!other.CompareTag("Player")) return;
        PlayerControl control = other.GetComponent<PlayerControl>();
        Player player = other.GetComponent<Player>();

        if (control == null) return;
        if (control.h + control.v < 0.1f) return;
        control.timer += Time.deltaTime;
        if (control.timer < 1f) return;
        control.timer = 0;
        //if (Random.Range(0, 100.0f) > 15) return;
        SetPokeMon();
        SetLevel();
        LevelStats();
        Debug.Log($"�̸�:{poke[pokeNum].name}, ����:{poke[pokeNum].Level}, ���ݷ�:{poke[pokeNum].CurrentAttack}, ����:{poke[pokeNum].CurrentDefense}, ü��:{poke[pokeNum].CurrentHP}");
        //BattleInfo info = new BattleInfo();
        //info.enemyIsHuman = false;
        //info.enemyPokemonList.Add(poke[pokeNum]);
        //Managers.Save.SaveJson<BattleInfo>(info);
        GameInfo info = new GameInfo();
        info.PlayerInfo = player.GetInfo();
        info.EnemyInfo = new AgentInfo();
        Pokemon pokemon = new GameObject { name = "wildPokemon" }.AddComponent<Pokemon>();
        pokemon.SetPokemon(poke[pokeNum]);
        info.EnemyInfo.PokemonList[0] = pokemon;
        info.PlayerInfo.position = other.transform.position;
        info.wildPokemon = true;
        Managers.Save.SaveJson(info);
        Managers.Scene.LoadScene(Define.Scene.Battle);
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
