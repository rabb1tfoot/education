using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    private OrderManager Order;
    private AudioManager Audio;
    private PlayerState playerState;
    private Inventory inven;
    private OOCScript ooc;
    public string key_sound;
    public string enter_sound;
    public string open_sound;
    public string close_sound;
    public string takeoff_sound;

    private enum EquipSlot
    {
        WEAPON = 0,
        SHILD,
        AMULT,
        LEFT_RING,
        RIGHT_RING,
        HELMET,
        ARMOR,
        LEFT_GLOVE,
        RIGHT_GLOVE,
        BELT,
        LEFT_BOOTS,
        RIGHT_BOOTS,
        END
    }

    public GameObject thisObject;
    public GameObject objOOC;
    public Text[] text; //스탯
    public Image[] img_slots; // 장비아이콘
    public GameObject ObjselectedSlotUI; //선택 슬롯아이콘

    public Item[] equipmentArr; //장비어레이

    private int selectedSlot; // 선택된 장비슬롯

    private bool activated = false;
    private bool inputKey = true;

    void Start()
    {
        inven = FindObjectOfType<Inventory>();
        Order = FindObjectOfType<OrderManager>();
        Audio = FindObjectOfType<AudioManager>();
        playerState = FindObjectOfType<PlayerState>();
        ooc = FindObjectOfType<OOCScript>();
    }

    public void EquipItem(Item _item)
    {
        string temp = _item.itemID.ToString();
        temp = temp.Substring(0, 3); //아이템ID자체를 앞세자리를 짤라서 종류별로 구분
        switch(temp)
        {
            case "200": //무기
                EquipItemCheck((int)EquipSlot.WEAPON, _item);
                break;
            case "201": //방패
                EquipItemCheck((int)EquipSlot.SHILD, _item);
                break;
            case "202": //아뮬렛
                EquipItemCheck((int)EquipSlot.AMULT, _item);
                break;
            case "203": //반지
                EquipItemCheck((int)EquipSlot.LEFT_RING, _item);
                break;

        }
    }

    public void EquipItemCheck(int _count, Item _item)
    {
        if(equipmentArr[_count].itemID != 0) //장착된걸 벗는다.
        {
            inven.EquipToInven(equipmentArr[_count]);
        }
        equipmentArr[_count] = _item;
    }

    public void SelectedSlot()
    {
        ObjselectedSlotUI.transform.position = img_slots[selectedSlot].transform.position;
    }

    public void ClearEquip()
    {
        Color color = img_slots[0].color;
        color.a = 0f;

        for(int i = 0; i < img_slots.Length; ++i)
        {
            img_slots[i].sprite = null;
            img_slots[i].color = color;
        }
    }

    public void ShowEquip()
    {
        Color color = img_slots [0].color;
        color.a = 1f;

        for (int i = 0; i < img_slots.Length; ++i)
        {
            if(equipmentArr[i].itemID != 0)
            {
                img_slots[i].sprite = equipmentArr[i].itemIcon;
                img_slots[i].color = color;
            }
        }
    }

    IEnumerator OOCCoroutine(string _up, string _down)
    {
        objOOC.SetActive(true);
        ooc.ShowChoice(_up, _down);
        yield return new WaitUntil(() => !ooc.activated);
        if (ooc.GetResult())
        {
            inven.EquipToInven(equipmentArr[selectedSlot]);
            equipmentArr[selectedSlot] = new Item(0, "", "", Item.ItemType.Equip);
            Audio.Play(takeoff_sound);
            ClearEquip();
            ShowEquip();
        }
        inputKey = true;
        objOOC.SetActive(false);
    }

    void Update()
    {
        if (inputKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                activated = !activated;

                if(activated)
                {
                    Order.NoMove();
                    Audio.Play(open_sound);
                    thisObject.SetActive(true);
                    selectedSlot = 0;
                    SelectedSlot();
                    ClearEquip();
                    ShowEquip();
                }
                else
                {
                    Order.CanMove();
                    Audio.Play(close_sound);
                    thisObject.SetActive(false);
                    ClearEquip();
                }
            }

            if(activated)
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Audio.Play(key_sound);
                    if (selectedSlot < img_slots.Length - 1)
                        selectedSlot++;
                    else
                        selectedSlot = 0;
                    SelectedSlot();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Audio.Play(key_sound);
                    if (selectedSlot > 0)
                        selectedSlot--;
                    else
                        selectedSlot = img_slots.Length - 1;
                    SelectedSlot();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Audio.Play(key_sound);
                    if (selectedSlot > 0)
                        selectedSlot--;
                    else
                        selectedSlot = img_slots.Length - 1;
                    SelectedSlot();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Audio.Play(key_sound);
                    if (selectedSlot < img_slots.Length - 1)
                        selectedSlot++;
                    else
                        selectedSlot = 0;
                    SelectedSlot();
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (equipmentArr[selectedSlot].itemID != 0)
                    {
                        Audio.Play(enter_sound);
                        inputKey = false;
                        StartCoroutine(OOCCoroutine("장착해체", "취소하기"));
                    }
                }
            }
        }
    }
}
