using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSystem : MonoBehaviour
{
    public AudioManager Audio;
    public string keySound;
    public string enterSound;
    public string cancelSound;
    public string correctSound;

    private int count; //배열크기
    private int selectedTextbox;
    private int selectedNum; //선택한 값
    private int correctNum; //정답

    public GameObject ParentObj;
    public GameObject[] panel;
    public Text[] NumberText;

    public Animator anim;

    public bool activated; //waitUntil대기
    private bool keyInput; //키 활성화
    private bool correctFlag; // 정답여부
    private string tempNumber;

    void Start()
    {
        Audio = FindObjectOfType<AudioManager>();
    }
    public void ShowNumber(int _correctNum)
    {
        correctNum = _correctNum;
        activated = true;
        correctFlag = false;

        string temp = correctNum.ToString();
        for(int i = 0; i < temp.Length; ++i)
        {
            panel[i].SetActive(true);
            NumberText[i].text = "0";
        }
        count = temp.Length -1;
        ParentObj.transform.position = new Vector3(ParentObj.transform.position.x + 30 * count
            , ParentObj.transform.position.y, ParentObj.transform.position.z);

        selectedTextbox = 0;
        selectedNum = 0;
        anim.SetBool("Appear", true);
        keyInput = true;
    }

    void Update()
    {
        if(keyInput)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                SetNum("DOWN");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SetNum("UP");
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (selectedTextbox == count)
                {
                    selectedTextbox = 0;
                }
                else
                {
                    selectedTextbox++;
                }
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (selectedTextbox == 0)
                {
                    selectedTextbox = count;
                }
                else
                {
                    selectedTextbox--;
                }
                SetColor();
            }
            else if(Input.GetKeyDown(KeyCode.Z))
            {
                Audio.Play(enterSound);
                keyInput = false;
                StartCoroutine(CheckAnswerCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Audio.Play(cancelSound);
                keyInput = false;
                StartCoroutine(ExitCoroutine());
            }
        }
    }
    IEnumerator CheckAnswerCoroutine()
    {
        Color color = NumberText[0].color;
        color.a = 1f;
        for (int i = count; i > -1; --i)
        {
            NumberText[i].color = color;
            tempNumber += NumberText[i].text;
        }

        yield return new WaitForSeconds(1f);

        selectedNum = int.Parse(tempNumber);

        if(selectedNum == correctNum)
        {
            Audio.Play(correctSound);
            correctFlag = true;
        }
        else
        {
            Audio.Play(cancelSound);
            correctFlag = false;
        }

        StartCoroutine(ExitCoroutine());
    }
    IEnumerator ExitCoroutine()
    {
        Debug.Log(GetResult());
        Debug.Log("선택: " +selectedNum +" 정답:" +correctNum);

        selectedNum = 0;
        tempNumber = "";
        anim.SetBool("Appear", false);
        yield return new WaitForSeconds(0.1f);

        for(int i=0; i< count; ++i)
        {
            panel[i].SetActive(false);
        }
        ParentObj.transform.position = new Vector3(ParentObj.transform.position.x - 30 * count
           , ParentObj.transform.position.y, ParentObj.transform.position.z);

        activated = false;
    }
    public void SetNum(string _arrow)
    {
        Audio.Play(keySound);

        int temp = int.Parse(NumberText[selectedTextbox].text);

        if (_arrow.Equals("DOWN"))
        {
            if (temp == 0)
                temp = 9;
            else
                temp--;

            SetColor();
        }
        else if(_arrow.Equals("UP"))
        {
            if (temp == 9)
                temp = 0;
            else
                temp++;

            SetColor();
        }

        NumberText[selectedTextbox].text = temp.ToString();
    }

    public void SetColor()
    {
        Audio.Play(keySound);
        Color color = NumberText[0].color;
        color.a = 0.3f;

        for(int i = 0; i< count + 1; ++i)
        {
            NumberText[i].color = color;
        }
        color.a = 1f;
        NumberText[selectedTextbox].color = color;
    }

    public bool GetResult()
    {
        return correctFlag;
    }
}
