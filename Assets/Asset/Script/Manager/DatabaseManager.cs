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
        //itemList.Add(new Item(10001, "����", "ü���� 50 ä���ݴϴ�.", Item.ItemType.Use));
        //itemList.Add(new Item(10002, "��������", "������ 15 ä���ݴϴ�.", Item.ItemType.Use));
        //itemList.Add(new Item(11001, "�����ڽ�", "�������� ���ɴϴ�..", Item.ItemType.Use));
        //itemList.Add(new Item(20001, "�ܰ�", "�⺻���� ��.", Item.ItemType.Equip, 2));
        //itemList.Add(new Item(20301, "����", "�Ϲ� �������� ����.", Item.ItemType.Equip, 0, 0, 1));
        //itemList.Add(new Item(30001, "��������", "��� ���� ����.", Item.ItemType.Quest));
        //itemList.Add(new Item(30003, "����", "�������� ������� ����.", Item.ItemType.Quest));
        //JsonManager.GetInstance().SaveItemJson(itemList);
        JsonManager.GetInstance().LoadItemJson();
    }
}
