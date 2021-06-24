using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instatnce;

    private PlayerState State;

    public GameObject prefabFloatingText;
    public GameObject parent;

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

    public void itemUse(int _ID)
    {
        switch (_ID)
        {
            case 10001:
                if (State.currentHp + 50 <= State.hp)
                    State.currentHp += 50;
                else
                    State.currentHp = State.hp;
                ShowFloatingText(50, "Green");
                break;
            case 10002:
                if (State.currentMp + 20 <= State.mp)
                    State.currentMp += 20;
                else
                    State.currentMp = State.hp;
                break;
        }
    }

    void ShowFloatingText(int _value, string _color = "White")
    {
        Vector3 vector = State.transform.position;
        vector.y += 60;

        GameObject clone = Instantiate(prefabFloatingText, vector, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().text.text = _value.ToString();
        if (_color == "Green")
        {
            clone.GetComponent<FloatingText>().text.color = Color.green;
        }
        else if(_color == "White")
        {
            clone.GetComponent<FloatingText>().text.color = Color.white;
        }
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.transform.SetParent(parent.transform);
    }

    void Start()
    {
        //State = FindObjectOfType<PlayerState>();
        //itemList.Add(new Item(10001, "포션", "체력을 50 채워줍니다.", Item.ItemType.Use));
        //itemList.Add(new Item(10002, "마나포션", "마력을 15 채워줍니다.", Item.ItemType.Use));
        //itemList.Add(new Item(11001, "랜덤박스", "랜덤으로 나옵니다..", Item.ItemType.Use));
        //itemList.Add(new Item(20001, "단검", "기본적인 검.", Item.ItemType.Equip, 2));
        //itemList.Add(new Item(20301, "반지", "일반 반지보단 좋음.", Item.ItemType.Equip, 0, 0, 1));
        //itemList.Add(new Item(30001, "유물조각", "고대 유물 파편.", Item.ItemType.Quest));
        //itemList.Add(new Item(30003, "유물", "파편으로 만들어진 유물.", Item.ItemType.Quest));
        //JsonManager.GetInstance().SaveItemJson(itemList);
        JsonManager.GetInstance().LoadItemJson();
    }
}
