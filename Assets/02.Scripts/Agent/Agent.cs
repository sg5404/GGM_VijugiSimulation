using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private const int maxPokemonCount = 6;

    protected Pokemon[] _pokemonList = new Pokemon[maxPokemonCount];
    protected Dictionary<Item, int> _itemDict = new Dictionary<Item, int>();

    public Agent()
    {
        for(int i = 0; i < _pokemonList.Length; i++)
        {
            _pokemonList[i] = null;
            _itemDict.Clear();
        }
    }

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

    public void SetPokemon(Pokemon[] list)
    {
        int maxIdx = Mathf.Min(list.Length, maxPokemonCount);
        for(int i = 0; i < maxIdx; i++)
        {
            _pokemonList[i] = list[i];
        }
    }

    public void SetPokemon(List<Pokemon> list)
    {
        int maxIdx = Mathf.Min(list.Count, maxPokemonCount);
        for (int i = 0; i < maxIdx; i++)
        {
            _pokemonList[i] = list[i];
        }
    }

    public void SetPokemonOfIndex(Pokemon pokemon, int index = 0)
    {
        if (index < 0 || index > 5)
        {
            Debug.LogError("index�� ������ �Ѿ�ϴ�.");
            return;
        }

        _pokemonList[index] = pokemon;
    }

    public Pokemon GetPokemon()
    {
        return GetPokemon(0);
    }

    public Pokemon GetPokemon(int index)
    {
        if (_pokemonList[index] == null) return null;
        return _pokemonList[index];
    }

    public void SwapPokemon(int fIdx, int sIdx)
    {
        Pokemon temp = _pokemonList[fIdx];
        _pokemonList[fIdx] = _pokemonList[sIdx];
        _pokemonList[sIdx] = temp;
    }

    public int EmptyPokemonIndex()
    {
        for(int i = 0; i < _pokemonList.Length; i++)
        {
            if (_pokemonList[i] == null)
                return i;
        }

        return -1;
    }

    /// <summary>
    /// ���� ���ִ���? ���� ����.
    /// </summary>
    /// <returns></returns>
    [System.Obsolete]
    public bool IsEmptyPokemonList()
    {
        for(int i = 0; i < _pokemonList.Length; i++)
        {
            if (_pokemonList[i] != null)
                return false;
        }

        return true;
    }

    /// <summary>
    /// ��� �ִ���? ���� ����.s
    /// </summary>
    /// <returns></returns>
    [System.Obsolete]
    public bool IsFullPokemonList()
    {
        for (int i = 0; i < _pokemonList.Length; i++)
        {
            if (_pokemonList[i] == null)
                return false;
        }

        return true;
    }

    public void ClearPokemon()
    {
        for(int i = 0; i < maxPokemonCount; i++)
        {
            _pokemonList[i] = null;
        }
    }

    public void ClearItem()
    {
        _itemDict.Clear();
    }
}
