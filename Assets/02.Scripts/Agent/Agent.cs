using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private const int maxPokemonCount = 6;

    protected string _name;
    protected Pokemon[] _pokemonList = new Pokemon[maxPokemonCount];
    //protected Dictionary<ItemSO, int> _itemDict = new Dictionary<ItemSO, int>();
    protected List<ItemPair> _itemList = new List<ItemPair>();

    public Agent()
    {
        _name = "???ֱ?";
        for(int i = 0; i < _pokemonList.Length; i++)
        {
            _pokemonList[i] = null;
            //_itemDict.Clear();
            _itemList.Clear();
        }
    }

    public void SetInfo(AgentInfo info)
    {
        this._name = info.Name;
        this._pokemonList = info.PokemonList;
        //this._itemDict = info.itemDict;
        this._itemList = info.itemList;
        this.transform.position = info.position;
    }

    public AgentInfo GetInfo()
    {
        AgentInfo info = new AgentInfo();
        info.Name = this._name;
        info.PokemonList = this._pokemonList;
        //info.itemDict = this._itemDict;
        info.itemList = this._itemList;
        info.position = this.transform.position;

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

    public ItemSO GetItem(int index = 0)
    {
        if (IsGetItem(index))
            return _itemList[index].item;

        return null;
    }

    public bool IsGetItem(int index)
    {
        return _itemList[index] != null;
    }

    public bool IsGetItem(ItemSO item)
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
            Debug.LogError("index?? ?????? ?Ѿ?ϴ?.");
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

    public bool IsEmptyPokemonList()
    {
        for(int i = 0; i < _pokemonList.Length; i++)
        {
            if (_pokemonList[i].Info != null)
                return false;
        }

        return true;
    }

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
