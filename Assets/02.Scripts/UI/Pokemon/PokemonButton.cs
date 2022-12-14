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

    private void Start()
    {
        _btn = GetComponent<Button>();
    }

    public void SetInfo(string name, Sprite image)
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

    public void AddEvent(UnityAction action)
    {
        _btn.onClick.AddListener(action);
    }
}
