using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    // Debug Code
    [SerializeField]
    private PokemonInfoSO _startPokemonInfo;
    [SerializeField]
    private SkillSO _skill;
    // Debug Code

    private void Start()
    {
        // Debug Code
        SetPokemonOfIndex(new Pokemon(_startPokemonInfo, 1));

        _pokemonList[0].SetSkill(_skill);
        //for(int i = 0; i < _pokemonList.Length; i++)
        //{
        //    Debug.Log($"{_pokemonList[i].Name}");
        //}

        //Debug.Log($"isFull : {IsFullPokemonList()}");
        //Debug.Log($"isEmpty : {IsEmptyPokemonList()}");

    }
}
