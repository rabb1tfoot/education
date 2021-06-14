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
    public Sprite itemIcon;
    public ItemType eType;
    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        ETC,
    }
    public Item(int _itemID, string _itemName, string _itemDes, ItemType _etype, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        eType = _etype;
        itemCount = _itemCount;
        string Path = "";
        CustomFunc.GetInstance().GetAssetPath(ref Path);
        Path += "\\ItemIcon";
        itemIcon = Resources.Load(Path + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
