using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class JsonManager : MonoBehaviour
{
    private static JsonManager instance;

    public static JsonManager GetInstance()
    {
        if (instance == null)
            instance = new JsonManager();
        return instance;
    }

    private JsonManager() { }

    //public class Data
    //{
    //    public int iLevel;
    //    public Vector3 v3_Position;
    //
    //
    //    public void printData()
    //    {
    //        Debug.Log("Level : " + iLevel);
    //        Debug.Log("position : " + v3_Position);
    //    }
    //}

    //void Start()
    //{
    //    Data data = new Data();
    //    data.iLevel = -12;
    //    data.v3_Position = new Vector3(1f, 2f, 3f);
    //    
    //    string str = JsonUtility.ToJson(data);
    //    
    //    Debug.Log("toJson" + str);
    //    
    //    Data data2 = JsonUtility.FromJson<Data>(str);
    //    data2.printData();
    //    
    //    //파일 세이브
    //    File.WriteAllText(Application.dataPath + "/TestJson.json", JsonUtility.ToJson(data));
    //    
    //    //파일로드
    //    string str2 = "불러온 파일 읽기:";
    //    str2 = File.ReadAllText(Application.dataPath + "/TestJson.json");
    //    
    //    Data data3 = JsonUtility.FromJson<Data>(str2);
    //    data3.printData();
    //}

    public void LoadItemJson()
    {
        string _path = "";
        CustomFunc.GetInstance().GetAssetPath(ref _path);
        _path += "ItemDB.json";
        string Jseonstring = File.ReadAllText(_path);
        JsonData itemData = JsonMapper.ToObject(Jseonstring);

        for(int i = 0; i< itemData.Count; ++i)
        {
            int ID = int.Parse(itemData[i]["itemID"].ToString());
            string name = itemData[i]["itemName"].ToString();
            string des = itemData[i]["itemDescription"].ToString();
            int count = int.Parse(itemData[i]["itemCount"].ToString());
            Item.ItemType type = (Item.ItemType)int.Parse(itemData[i]["eType"].ToString());

            DatabaseManager.instatnce.itemList.Add(new Item(ID, name, des, type, count, true));
        }
    
    }
    public void SaveItemJson(List<Item> _saveObj)
    {
        string _path = "";
        CustomFunc.GetInstance().GetAssetPath(ref _path);
        _path += "ItemDB.json";
        JsonData ItemJson = JsonMapper.ToJson(_saveObj);
        File.WriteAllText(_path, ItemJson.ToString());
    }
}
