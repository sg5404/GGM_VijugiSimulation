using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    [SerializeField]
    private PokemonInfoSO _startPokemonInfo;

    private void Start()
    {
        SetPokemonOfIndex(new Pokemon(_startPokemonInfo, 1));
        for(int i = 0; i < _pokemonList.Length; i++)
        {
            Debug.Log($"{_pokemonList[i].Name}");
        }

        //Debug.Log($"isFull : {IsFullPokemonList()}");
        //Debug.Log($"isEmpty : {IsEmptyPokemonList()}");

    }
}
