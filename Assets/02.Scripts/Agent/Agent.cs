using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private const int maxPokemonCount = 6;

    protected Pokemon[] _pokemonList = new Pokemon[maxPokemonCount];
    //protected Dictionary<ItemSO, int> _itemDict = new Dictionary<ItemSO, int>();
    protected List<ItemPair> _itemList = new List<ItemPair>();

    public Agent()
    {
        for(int i = 0; i < _pokemonList.Length; i++)
        {
            _pokemonList[i] = null;
            //_itemDict.Clear();
            _itemList.Clear();
        }
    }

    public void SetInfo(AgentInfo info)
    {
        this._pokemonList = info.PokemonList;
        //this._itemDict = info.itemDict;
        this._itemList = info.itemList;
    }

    public AgentInfo GetInfo()
    {
        AgentInfo info = new AgentInfo();
        info.PokemonList = this._pokemonList;
        //info.itemDict = this._itemDict;
        info.itemList = this._itemList;

        return info;
    }

    public void SetItem(ItemSO item, int cnt = 1)
    {
        //if (_itemDict.ContainsKey(item))
        //{
        //    this._itemDict[item] += cnt;
        //}
        //else
        //{
        //    this._itemDict.Add(item, cnt);
        //}

        if (IsGetItem(item) == true)
        {
            _itemList[IsGetItemIndex(item)].cnt += cnt;
        }
        else
        {
            _itemList.Add(new ItemPair(item, cnt));
        }
    }

    private bool IsGetItem(ItemSO item)
    {
        foreach(var i in _itemList)
        {
            if (i.item == item) return true;
        }

        return false;
    }

    private int IsGetItemIndex(ItemSO item)
    {
        for(int i = 0; i < _itemList.Count; i++)
        {
            if (_itemList[i].item == item) return i;
        }

        return -1;
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
            Debug.LogError("index가 범위를 넘어갑니다.");
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
    /// 가득 차있는지? 버그 있음.
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
    /// 비어 있는지? 버그 있음.s
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
        //_itemDict.Clear();
        _itemList.Clear();
    }
}
