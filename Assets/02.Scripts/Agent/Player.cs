using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    // Debug Code
    [SerializeField]
    private List<PokemonInfoSO> _startPokemonInfo;
    [SerializeField]
    private SkillSO _skill;
    // Debug Code

    private void Start()
    {
        // Debug Code
        for(int i = 0; i < _startPokemonInfo.Count; i++)
        {
            SetPokemonOfIndex(new Pokemon(_startPokemonInfo[i], 3), i);
        }

        _pokemonList[0].SetSkill(_skill);
        
        //for(int i = 0; i < _pokemonList.Length; i++)
        //{
        //    Debug.Log($"{_pokemonList[i].Name}");
        //}

        //Debug.Log($"isFull : {IsFullPokemonList()}");
        //Debug.Log($"isEmpty : {IsEmptyPokemonList()}");

    }
}
