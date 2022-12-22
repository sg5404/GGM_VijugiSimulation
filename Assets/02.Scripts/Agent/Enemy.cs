using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{
    [SerializeField]
    private string enemyName;
    [SerializeField]
    private List<PokemonInfoSO> _startPokemonInfo;
    [SerializeField]
    private int _pokemonLevel;
    [SerializeField]
    private string _battleStartText;
    public string BattleStartText => _battleStartText;

    private void Start()
    {
        for (int i = 0; i < _startPokemonInfo.Count; i++)
        {
            SetPokemonOfIndex(new Pokemon(_startPokemonInfo[i], _pokemonLevel), i);
        }

        _name = enemyName;
    }
}
