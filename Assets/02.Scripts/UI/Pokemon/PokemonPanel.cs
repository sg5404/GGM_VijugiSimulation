using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonPanel : MonoBehaviour
{
    private const int MAX_POKEMON_CNT = 6;

    private Pokemon[] _pokemonList = new Pokemon[MAX_POKEMON_CNT];
    private PokemonButton[] _pokemonBtnList = new PokemonButton[MAX_POKEMON_CNT];

    private void OnEnable()
    {
        for(int i = 0; i < MAX_POKEMON_CNT; i++)
        {
            _pokemonBtnList[i] = transform.GetChild(i).GetComponent<PokemonButton>();
        }
    }

    public void SetPokeon(Pokemon[] list)
    {
        for (int i = 0; i < MAX_POKEMON_CNT; i++)
        {
            _pokemonList[i] = list[i];
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        for(int i = 0; i < MAX_POKEMON_CNT; i++)
        {
            if(_pokemonList[i] != null)
            {
                _pokemonBtnList[i]?.SetInfo(_pokemonList[i].Name, _pokemonList[i].Info.image);
            }
            else
            {
                _pokemonBtnList[i]?.SetInfo("", null);
            }
        }
    }
}
