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
        //itemList.Add(new Item(10001, "����", "ü���� 50 ä���ݴϴ�.", Item.ItemType.Use));
        //itemList.Add(new Item(10002, "��������", "������ 15 ä���ݴϴ�.", Item.ItemType.Use));
        //itemList.Add(new Item(11001, "�����ڽ�", "�������� ���ɴϴ�..", Item.ItemType.Use));
        //itemList.Add(new Item(20001, "�ܰ�", "�⺻���� ��.", Item.ItemType.Equip));
        //itemList.Add(new Item(21001, "����", "�Ϲ� �������� ����.", Item.ItemType.Equip));
        //itemList.Add(new Item(30001, "��������", "��� ���� ����.", Item.ItemType.Quest));
        //itemList.Add(new Item(30003, "����", "�������� ������� ����.", Item.ItemType.Quest));
        //JsonManager.GetInstance().SaveItemJson(itemList);
        JsonManager.GetInstance().LoadItemJson();
    }
}
