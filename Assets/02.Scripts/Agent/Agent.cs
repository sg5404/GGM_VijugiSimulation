using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    protected Pokemon[] _pokemonList = new Pokemon[6];
    protected Dictionary<Item, int> _itemDict = new Dictionary<Item, int>();

    public void SetInfo(AgentInfo info)
    {
        this._pokemonList = info.PokemonList;
        this._itemDict = info.itemDict;
    }

    public AgentInfo GetInfo()
    {
        AgentInfo info = new AgentInfo();
        info.PokemonList = this._pokemonList;
        info.itemDict = this._itemDict;

        return info;
    }
}
