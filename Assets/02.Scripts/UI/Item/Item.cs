using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    protected Button _btn;

    protected Image _image;
    protected Text _itemName;
    protected Text _itemCnt;

    private ItemSO _itemSO;
    private int _cnt;

    protected virtual void OnEnable()
    {
        _btn = transform.Find("bg").GetComponent<Button>();
    }

    public void SetItem(ItemSO item, int cnt = 1)
    {
        _itemSO = item;
        _cnt = cnt;

        _itemCnt.text = cnt.ToString();
        _image = _itemSO.image;
        _itemName.text = _itemSO.name;
    }
}
