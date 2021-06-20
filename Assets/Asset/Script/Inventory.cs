using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private DatabaseManager DBManager;
    private OrderManager Order;
    private AudioManager Audio;
    private OOCScript OOC;
    public string keySound;
    public string enterSound;
    public string cancelSound;
    public string openSound;
    public string beepSound;

    private InventorySlot[] slots;

    private List<Item> inventoryItemList; //�÷��̾ ������ ������ ����Ʈ
    private List<Item> inventoryTabList; //������ ���� ����Ʈ

    public Text descriptionText; //������ ����
    public string[] tabDescription; //�ܼ���

    public Transform invenUIPos; //slot�� �θ�

    public GameObject thisObj; //Ȱ��ȭ ��Ȱ��ȭ
    public GameObject[] selectedTabImages; //�� ���Ե�
    public GameObject GO_OOC; // ������ Ȱ��ȭ ��Ȱ��ȭ
    public GameObject prefabFloatingText;

    private int selectedItem;
    private int selectedTab;

    private bool activated;
    private bool tabActivated;
    private bool itemActivated;
    private bool stopKeyInput;
    private bool preventExec; //�ߺ����� ����

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    void Start()
    {
        instance = this;
        DBManager = FindObjectOfType<DatabaseManager>();
        Audio = FindObjectOfType<AudioManager>();
        Order = FindObjectOfType<OrderManager>();
        OOC = FindObjectOfType<OOCScript>();
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = invenUIPos.GetComponentsInChildren<InventorySlot>();

        
    }

    public void GetItem(int _ID, int _count = 1)
    {
        for (int i = 0; i < DBManager.itemList.Count; i++)
        {
            if(_ID == DBManager.itemList[i].itemID)
            {

                var clone = Instantiate(prefabFloatingText, PlayerManager.instatnce.transform.position,
                    Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = DBManager.itemList[i].itemName + " " + _count + "�� ȹ��";

                clone.transform.SetParent(this.transform);

                for(int j =0; j < inventoryItemList.Count; ++j)
                {
                    if(inventoryItemList[j].itemID == _ID)
                    {
                        if (inventoryItemList[j].eType == Item.ItemType.Use)
                        {
                            inventoryItemList[j].itemCount += _count;
                        }
                        else
                        {
                            inventoryItemList.Add(DBManager.itemList[i]);
                        }
                        return;
                    }
                }
                inventoryItemList.Add(DBManager.itemList[i]);
                inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }
        Debug.LogError("�ش� �������� DB�� ����");
    }

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    }
    public void RemoveSlot()
    {
        for(int i = 0; i< slots.Length; ++i)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    }
    public void SelectedTab()
    {
        StopAllCoroutines();

        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for(int i = 0; i < selectedTabImages.Length; ++i)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        descriptionText.text = tabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroutine());
    }

    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch(selectedTab)
        {
            case 0:
                for(int i = 0; i < inventoryItemList.Count; ++i)
                {
                    if ((Item.ItemType.Use == inventoryItemList[i].eType))
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 1:
                for (int i = 0; i < inventoryItemList.Count; ++i)
                {
                    if ((Item.ItemType.Equip == inventoryItemList[i].eType))
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 2:
                for (int i = 0; i < inventoryItemList.Count; ++i)
                {
                    if ((Item.ItemType.Quest == inventoryItemList[i].eType))
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 3:
                for (int i = 0; i < inventoryItemList.Count; ++i)
                {
                    if ((Item.ItemType.ETC == inventoryItemList[i].eType))
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
        }

        for(int i = 0; i < inventoryTabList.Count; ++i)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        }

        SeletctedItem();
    }

    public void SeletctedItem()
    {
        StopAllCoroutines();
        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].selectedItem.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryItemList.Count; ++i)
                slots[i].selectedItem.GetComponent<Image>().color = color;
            StartCoroutine(SelectedItemEffectCoroutine());

            descriptionText.text = inventoryTabList[selectedItem].itemDescription;
        }
        else
            descriptionText.text = "�������� �����ϴ�.";
    }

    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopKeyInput)
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("i�Է�");

                activated = !activated;

                if(activated)
                {
                    Audio.Play(openSound);
                    Order.NoMove();
                    thisObj.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    Audio.Play(cancelSound);
                    StopAllCoroutines();
                    Order.CanMove();
                    thisObj.SetActive(false);
                    selectedTab = 0;
                    tabActivated = false;
                    itemActivated = false;
                }
            }

            if(activated)
            {
                if(tabActivated)
                {
                    if(Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                            selectedTab++;
                        else
                            selectedTab = 0;
                        Audio.Play(keySound);
                        SelectedTab();
                    }

                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                            selectedTab--;
                        else
                            selectedTab = selectedTabImages.Length - 1;
                        Audio.Play(keySound);
                        SelectedTab();
                    }
                    else if(Input.GetKeyDown(KeyCode.Z))
                    {
                        Audio.Play(enterSound);
                        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<Image>().color = color;

                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true;

                        ShowItem();
                    }
                }
                else if(itemActivated)
                {
                    if(inventoryTabList.Count> 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 2)
                                selectedItem += 2;
                            else
                                selectedItem %= 2;
                            Audio.Play(keySound);
                            SelectedTab();
                        }
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 2;
                            else
                                selectedItem = inventoryTabList.Count - 1 - selectedItem;
                            Audio.Play(keySound);
                            SelectedTab();
                        }
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            Audio.Play(keySound);
                            SelectedTab();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = inventoryTabList.Count - 1;
                            Audio.Play(keySound);
                            SelectedTab();
                        }
                        if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                        {
                            if (selectedTab == 0) //�Ҹ�ǰ
                            {
                                Audio.Play(enterSound);
                                stopKeyInput = true;
                                StartCoroutine(OOCCoroutine());

                            }
                            else if (selectedTab == 1) //���ǰ
                            {

                            }
                            else
                            {
                                Audio.Play(beepSound);
                            }
                        }                      
                    }
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        Audio.Play(cancelSound);
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }

                    if (Input.GetKeyUp(KeyCode.Z)) //�ߺ����� ����
                    {
                        preventExec = false;
                    }
                }
                   
                
            }
        }
    }

    IEnumerator OOCCoroutine()
    {
        GO_OOC.SetActive(true);
        OOC.ShowChoice("����ϱ�", "����ϱ�");
        yield return new WaitUntil(() => !OOC.activated);
        if(OOC.GetResult())
        {
            for(int i = 0; i< inventoryItemList.Count; ++i)
            {
                if(inventoryItemList[i].itemID == inventoryTabList[selectedItem].itemID)
                {
                    DBManager.itemUse(inventoryItemList[i].itemID);

                    if (inventoryItemList[i].itemCount > 1)
                        inventoryItemList[i].itemCount--;
                    else
                    {
                        inventoryItemList.RemoveAt(i);
                    }
                    ShowItem();
                    break;
                }
            }
        }
        stopKeyInput = false;
        GO_OOC.SetActive(false);
    }
}
