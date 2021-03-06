using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public string itemDescription; // 아이템 설명
    public int itemCount;
    public ItemType eType;
    public Sprite itemIcon;
    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        ETC,
    }

    public int atk;
    public int def;
    public int recoverHp;
    public int recoverMp;


    public Item(int _itemID, string _itemName, string _itemDes, ItemType _etype
        , int _atk = 0, int _def = 0, int _recoverHp = 0, int _recoverMp = 0, int _itemCount = 1, bool _bLoading = false)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemCount = _itemCount;
        eType = _etype;
        if (_bLoading)
            itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
        else
            itemIcon = null;

        atk = _atk;
        def = _def;
        recoverHp = _recoverHp;
        recoverMp = _recoverMp;
    }
}
