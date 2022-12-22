using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class ItemPanel : MonoBehaviour
{
    private List<ItemPair> _itemDict = new List<ItemPair>();

    [SerializeField]
    private Transform _parent;

    private List<Item> _itemList = new List<Item>();

    private void OnEnable()
    {
        Clear();

        CreateItem();
    }

    private bool IsGetItem(ItemSO item)
    {
        foreach (var i in _itemDict)
        {
            if (i.item == item) return true;
        }

        return false;
    }

    private int IsGetItemIndex(ItemSO item)
    {
        for (int i = 0; i < _itemDict.Count; i++)
        {
            if (_itemDict[i].item == item) return i;
        }

        return -1;
    }

    public void SetList(List<ItemPair> list)
    {
        //_itemDict = dict;
        _itemDict = list;

        Clear();

        CreateItem();
    }

    public void AddItem(ItemSO item, int cnt = 1)
    {
        //if(_itemDict.ContainsKey(item))
        //{
        //    _itemDict[item]++;
        //}
        //else
        //{
        //    _itemDict.Add(item, 1);
        //}

        if(IsGetItem(item) == true)
        {
            _itemDict[IsGetItemIndex(item)].cnt += cnt;
        }
        else
        {
            _itemDict.Add(new ItemPair(item, cnt));
        }
    }

    public void RemoveItem(ItemSO item, int cnt = 1)
    {
        //if (_itemDict.ContainsKey(item))
        //{
        //    _itemDict[item] -= cnt;
        //}

        if(IsGetItem(item) == true)
        {
            _itemDict[IsGetItemIndex(item)].cnt -= cnt;
        }
    }

    public void RemoveItem(int index, int cnt = 1)
    {
        if (_itemDict[index] != null)
        {
            _itemDict[index].cnt -= cnt;

            if (_itemDict[index].cnt <= 0)
            {
                _itemDict.RemoveAt(index);
            }
        }
    }

    public void CreateItem()
    {
        for(int i = 0; i < _itemDict.Count; i++)
        {
            Item item = Managers.Resource.Instantiate("UI/Item", _parent).GetComponent<Item>();
            item.SetItem(_itemDict[i].item, _itemDict[i].cnt);
            _itemList.Add(item);

        }
    }

    public void AddAllItemEvent(UnityAction action = null)
    {
        foreach(var item in _itemList)
        {
            item.AddEvent(action);
        }
    }

    public void Clear()
    {
        foreach(var item in _itemList)
        {
            Poolable p = item.GetComponent<Poolable>();
            Managers.Pool.Push(p);
        }

        _itemList.Clear();
    }
}
