using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public int iLevel;
        public Vector3 v3_Position;


        public void printData()
        {
            Debug.Log("Level : " + iLevel);
            Debug.Log("position : " + v3_Position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Data data = new Data();
        data.iLevel = -12;
        data.v3_Position = new Vector3(1f, 2f, 3f);

        string str = JsonUtility.ToJson(data);

        Debug.Log("toJson" + str);

        Data data2 = JsonUtility.FromJson<Data>(str);
        data2.printData();

        //Application.dataPath ==> Asset����
        //���� ���̺�
        File.WriteAllText(Application.dataPath + "/TestJson.json", JsonUtility.ToJson(data));

        //���Ϸε�
        string str2 = "�ҷ��� ���� �б�:";
        str2 = File.ReadAllText(Application.dataPath + "/TestJson.json");

        Data data3 = JsonUtility.FromJson<Data>(str2);
        data3.printData();
    }

    void LoadJson(string _path, object _saveObj)
    {

    }
    void SaveItemJson(string _path, object _saveObj)
    {
        CustomFunc.GetInstance().GetAssetPath(ref _path);
        File.WriteAllText(_path + "/ItemDB.json", JsonUtility.ToJson());
    }
}
