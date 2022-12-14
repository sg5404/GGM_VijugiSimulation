using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonChoicePanel : MonoBehaviour
{
    private const int MAX_POKEMON_CNT = 6;

    private Pokemon[] _pokemonList = new Pokemon[MAX_POKEMON_CNT];
    private PokemonButton[] _pokemonBtnList = new PokemonButton[MAX_POKEMON_CNT];

    private void OnEnable()
    {
        for (int i = 0; i < MAX_POKEMON_CNT; i++)
        {
            _pokemonBtnList[i] = transform.GetChild(i).GetComponent<PokemonButton>();
        }
    }

    public void SetPokeom(Pokemon[] list)
    {
        for (int i = 0; i < MAX_POKEMON_CNT; i++)
        {
            _pokemonList[i] = list[i];
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < MAX_POKEMON_CNT; i++)
        {
            int index = i;
            if (_pokemonList[index].Info != null)
            {
                _pokemonBtnList[index].SetPokemon(_pokemonList[i]);
            }
            else
            {
                _pokemonBtnList[index].SetPokemon(null);
            }

            _pokemonBtnList[index].AddEvent(() =>
            {
                BattleScene scene = Managers.Scene.CurrentScene as BattleScene;
                scene.ReturnThrowIndex(index);
            });
        }
    }
}
