using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class SaveLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public float playerX;
        public float playerY;
        public float playerZ;

        public int playerLv;
        public int playerHp;
        public int playerMp;

        public int playerCHp;
        public int playerCMp;
        public int playerCExp;

        public int playerATK;
        public int playerDEF;

        public int playerAddATK;
        public int playerAddDEF;
        public int playerAddHp;
        public int playerAddMp;

        public List<int> playerItemInventory;
        public List<int> playerIteminventoryCount;
        public List<int> playerEquipItem;

        public string mapName;
        public string sceneName;
        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNameList;
        public List<float> varNumberList;
    }

    private PlayerManager Player;
    private PlayerState PlayerState;
    private DatabaseManager DB;
    private Inventory Inven;
    private Equipment Equip;
    private FadeManager Fade;

    public Data data;

    private Vector3 pvector;


    public void CallSave()
    {
        DB = FindObjectOfType<DatabaseManager>();
        Player = FindObjectOfType<PlayerManager>();
        PlayerState = FindObjectOfType<PlayerState>();
        Inven = FindObjectOfType<Inventory>();
        Equip = FindObjectOfType<Equipment>();

        data.playerX = Player.transform.position.x;
        data.playerY = Player.transform.position.y;
        data.playerZ = Player.transform.position.z;

        data.playerLv = PlayerState.Lv;
        data.playerHp = PlayerState.hp;
        data.playerMp = PlayerState.mp;
        data.playerCHp = PlayerState.currentHp;
        data.playerCMp = PlayerState.currentMp;
        data.playerCExp = PlayerState.currentExp;
        data.playerATK = PlayerState.atk;
        data.playerDEF = PlayerState.def;

        data.mapName = Player.currentMapName;
        data.sceneName = Player.sceneName;

        data.playerItemInventory.Clear(); //기존 아이템을 초기화시켜준다.
        data.playerIteminventoryCount.Clear();
        data.playerEquipItem.Clear();

        for(int i = 0; i < DB.var_name.Length; ++i)
        {
            data.varNameList.Add(DB.var_name[i]);
            data.varNumberList.Add(DB.var[i]);
        }

        for (int i = 0; i < DB.switch_name.Length; ++i)
        {
            data.swList.Add(DB.switches[i]);
            data.swNameList.Add(DB.switch_name[i]);
        }

        List<Item> itemList = Inven.SaveItem();

        for(int i = 0; i < itemList.Count; ++i)
        {
            data.playerItemInventory.Add(itemList[i].itemID);
            data.playerIteminventoryCount.Add(itemList[i].itemCount);
        }

        for(int i = 0; i < Equip.equipmentArr.Length; ++i)
        {
            data.playerEquipItem.Add(Equip.equipmentArr[i].itemID);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.dataPath + "/SaveFile.dat");

        bf.Serialize(fs, data);
        fs.Close();

        Debug.Log(Application.dataPath + "에 저장완료");

    }
    public void CallLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);

        if(fs != null && fs.Length > 0)
        {
            data = (Data)bf.Deserialize(fs);

            DB = FindObjectOfType<DatabaseManager>();
            Player = FindObjectOfType<PlayerManager>();
            PlayerState = FindObjectOfType<PlayerState>();
            Inven = FindObjectOfType<Inventory>();
            Equip = FindObjectOfType<Equipment>();
            Fade = FindObjectOfType<FadeManager>();

            Fade.FadeOut();

            Player.currentMapName = data.mapName;
            Player.sceneName = data.sceneName;

            PlayerState.Lv = data.playerLv;
            PlayerState.hp = data.playerHp;
            PlayerState.mp = data.playerMp;
            PlayerState.currentHp = data.playerCHp;
            PlayerState.currentMp = data.playerCMp;
            PlayerState.currentExp = data.playerCExp;
            PlayerState.atk = data.playerATK;
            PlayerState.def = data.playerDEF;

            Player.transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);

            DB.var = data.varNumberList.ToArray();
            DB.var_name = data.varNameList.ToArray();

            DB.switches = data.swList.ToArray();
            DB.switch_name = data.swNameList.ToArray();

            for(int i = 0; i< Equip.equipmentArr.Length; ++i)
            {
                for(int j = 0; j < DB.itemList.Count; ++j)
                {
                    if(data.playerEquipItem[i] == DB.itemList[j].itemID)
                    {
                        Equip.equipmentArr[i] = DB.itemList[j];
                        break;
                    }
                }
            }
            List<Item> itemList = new List<Item>();

            for (int i = 0; i < data.playerItemInventory.Count; ++i)
            {
                for (int j = 0; j < DB.itemList.Count; ++j)
                {
                    if (data.playerItemInventory[i] == DB.itemList[j].itemID)
                    {
                        itemList.Add(DB.itemList[j]);
                        break;
                    }
                }
            }
            for(int i = 0; i < data.playerIteminventoryCount.Count; i++)
            {
                itemList[i].itemCount = data.playerIteminventoryCount[i];
            }

            Inven.LoadItem(itemList);
            Equip.ShowText();
            StartCoroutine(WaitCoroutine());

        }
        else
        {
            Debug.Log("로드실패");
        }
        fs.Close();
        Debug.Log(Application.dataPath + "에 저장완료");
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1f);

        GameManager GM = FindObjectOfType<GameManager>();
        GM.LoadStart();

        SceneManager.LoadScene(data.sceneName);
    }

}
