using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonTrainer : MonoBehaviour
{
    [SerializeField]
    private string _trainerName;
    [SerializeField]
    private int _pokemonLevel;
    [SerializeField]
    private List<PokemonInfoSO> _pokemonList = new List<PokemonInfoSO>();
}
