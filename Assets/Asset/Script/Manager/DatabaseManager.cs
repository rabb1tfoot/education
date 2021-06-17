using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instatnce;

    private void Awake()
    {
        if (instatnce == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instatnce = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();

    void Start()
    {
        //itemList.Add(new Item(10001, "포션", "체력을 50 채워줍니다.", Item.ItemType.Use));
        //itemList.Add(new Item(10002, "마나포션", "마력을 15 채워줍니다.", Item.ItemType.Use));
        //itemList.Add(new Item(11001, "랜덤박스", "랜덤으로 나옵니다..", Item.ItemType.Use));
        //itemList.Add(new Item(20001, "단검", "기본적인 검.", Item.ItemType.Equip));
        //itemList.Add(new Item(21001, "반지", "일반 반지보단 좋음.", Item.ItemType.Equip));
        //itemList.Add(new Item(30001, "유물조각", "고대 유물 파편.", Item.ItemType.Quest));
        //itemList.Add(new Item(30003, "유물", "파편으로 만들어진 유물.", Item.ItemType.Quest));
        //JsonManager.GetInstance().SaveItemJson(itemList);
        JsonManager.GetInstance().LoadItemJson();
    }
}
