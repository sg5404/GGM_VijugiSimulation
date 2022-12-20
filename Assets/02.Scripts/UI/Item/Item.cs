using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected Button _btn;
    
    [SerializeField] protected Image _iamgeImage;
    [SerializeField] protected Text _itemName;
    [SerializeField] protected Text _itemCnt;

    private ItemSO _itemSO;
    private int _cnt;

    public void SetItem(ItemSO item, int cnt = 1)
    {
        _itemSO = item;
        _cnt = cnt;

        _itemCnt.text = "x " + _cnt.ToString();
        _iamgeImage.sprite = _itemSO.image;
        _itemName.text = _itemSO.name;
    }

    public void AddEvent(UnityAction action)
    {
        _btn.onClick.AddListener(action);
    }
}
