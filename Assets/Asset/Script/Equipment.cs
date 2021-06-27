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

    public enum STATE
    {
        ATK = 0,
        DEF,
        HPRECOVER = 6,
        MPRECOVER
    }

    private int added_atk, added_def, addedRHP, addedRMP;

    public GameObject thisObject;
    public GameObject objOOC;
    public Text[] text; //����
    public Image[] img_slots; // ��������
    public GameObject ObjselectedSlotUI; //���� ���Ծ�����

    public Item[] equipmentArr; //�����

    private int selectedSlot; // ���õ� ��񽽷�

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

    public void ShowText()
    {
        if(added_atk == 0)
            text[(int)STATE.ATK].text = playerState.atk.ToString();
        else
            text[(int)STATE.ATK].text = playerState.atk.ToString() + "(+" + added_atk + ")";

        if (added_def == 0)
            text[(int)STATE.DEF].text = playerState.def.ToString();
        else
            text[(int)STATE.DEF].text = playerState.def.ToString() + "(+" + added_def + ")";

        if (addedRHP == 0)
            text[(int)STATE.HPRECOVER].text = playerState.recoverHp.ToString();
        else
            text[(int)STATE.HPRECOVER].text = playerState.recoverHp.ToString() + "(+" + addedRHP + ")";

        if (addedRMP == 0)
            text[(int)STATE.MPRECOVER].text = playerState.recoverMp.ToString();
        else
            text[(int)STATE.MPRECOVER].text = playerState.recoverMp.ToString() + "(+" + addedRMP + ")";
    }

    public void EquipItem(Item _item)
    {
        string temp = _item.itemID.ToString();
        temp = temp.Substring(0, 3); //������ID��ü�� �ռ��ڸ��� ©�� �������� ����
        switch(temp)
        {
            case "200": //����
                EquipItemCheck((int)EquipSlot.WEAPON, _item);
                break;
            case "201": //����
                EquipItemCheck((int)EquipSlot.SHILD, _item);
                break;
            case "202": //�ƹķ�
                EquipItemCheck((int)EquipSlot.AMULT, _item);
                break;
            case "203": //����
                EquipItemCheck((int)EquipSlot.LEFT_RING, _item);
                break;

        }
    }

    public void EquipItemCheck(int _count, Item _item)
    {
        if(equipmentArr[_count].itemID != 0) //�����Ȱ� ���´�.
        {
            inven.EquipToInven(equipmentArr[_count]);
        }
        equipmentArr[_count] = _item;

        EquipTakeOn(_item);
        ShowText();
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
            EquipTakeOff(equipmentArr[selectedSlot]);
            equipmentArr[selectedSlot] = new Item(0, "", "", Item.ItemType.Equip);
            Audio.Play(takeoff_sound);
            ClearEquip();
            ShowEquip();
            ShowText();
        }
        inputKey = true;
        objOOC.SetActive(false);
    }

    private void EquipTakeOn(Item _item)
    {
        playerState.atk += _item.atk;
        playerState.def += _item.def;
        playerState.recoverMp += _item.recoverMp;
        playerState.recoverHp += _item.recoverHp;

        added_atk += _item.atk;
        added_def += _item.def;
        addedRMP+= _item.recoverMp;
        addedRHP += _item.recoverHp;
    }
    private void EquipTakeOff(Item _item)
    {
        playerState.atk -= _item.atk;
        playerState.def -= _item.def;
        playerState.recoverMp -= _item.recoverMp;
        playerState.recoverHp -= _item.recoverHp;

        added_atk -= _item.atk;
        added_def -= _item.def;
        addedRMP -= _item.recoverMp;
        addedRHP -= _item.recoverHp;
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
                    ShowText();
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
                        StartCoroutine(OOCCoroutine("������ü", "����ϱ�"));
                    }
                }
            }
        }
    }
}
