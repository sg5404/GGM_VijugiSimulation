using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    private Dictionary<ItemSO, int> _itemDict = new Dictionary<ItemSO, int>();

    [SerializeField]
    private Transform _parent;

    private List<Item> _itemList = new List<Item>();

    private void OnEnable()
    {
        Clear();

        CreateItem();
    }

    public void SetDict(Dictionary<ItemSO, int> dict)
    {
        _itemDict = dict;

        Clear();

        CreateItem();
    }

    public void AddItem(ItemSO item)
    {
        if(_itemDict.ContainsKey(item))
        {
            _itemDict[item]++;
        }
        else
        {
            _itemDict.Add(item, 1);
        }
    }

    public void RemveItem(ItemSO item, int cnt = 1)
    {
        if (_itemDict.ContainsKey(item))
        {
            _itemDict[item] -= cnt;
        }
    }

    public void CreateItem()
    {
        foreach(var item in _itemDict)
        {
            Item i = Managers.Resource.Instantiate("UI/Item", _parent).GetComponent<Item>();
            i.SetItem(item.Key, item.Value);
            _itemList.Add(i);
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
