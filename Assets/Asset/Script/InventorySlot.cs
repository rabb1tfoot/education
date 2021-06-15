using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text itemNameText;
    public Text itemCountText;
    public GameObject selectedItem;
    
    public void Additem(Item _item)
    {
        itemNameText.text = _item.itemName;
        icon.sprite = _item.itemIcon;
        if(_item.eType == Item.ItemType.Use)
        {
            if (_item.itemCount > 0)
                itemCountText.text = "x" + _item.itemCount.ToString();
            else
                itemCountText.text = "";
        }
    }

    public void RemoveItem()
    {
        icon.sprite = null;
        itemNameText.text = "";
        itemCountText.text = "";

    }
}
