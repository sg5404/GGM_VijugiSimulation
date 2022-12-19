using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PokemonButton : MonoBehaviour
{
    [SerializeField]
    private Text _pokemonName;
    [SerializeField]
    private Image _pokemonImage;

    private Button _btn;

    private Pokemon _pokemon;

    private void OnEnable()
    {
        _btn = GetComponent<Button>();
    }

    private void SetInfo(string name, Sprite image)
    {
        if (name == "" || image == null || name == null)
        {
            _pokemonName.text = "";
            _pokemonImage.gameObject.SetActive(false);
        }
        else
        {
            _pokemonImage.gameObject.SetActive(true);
            _pokemonName.text = name;
            _pokemonImage.sprite = image;
        }
    }

    public void SetPokemon(Pokemon pokemon)
    {
        this._pokemon = pokemon;
        if(pokemon != null)
        {
            SetInfo(_pokemon.Name, _pokemon.Info.image);
        }
        else
        {
            SetInfo("", null);
        }
    }

    public Pokemon GetPokemon()
    {
        return _pokemon;
    }

    public void AddEvent(UnityAction action)
    {
        _btn.onClick.RemoveAllListeners();

        _btn.onClick.AddListener(action);
    }
}
