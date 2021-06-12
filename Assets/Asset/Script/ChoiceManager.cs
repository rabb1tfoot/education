using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    private AudioManager Audio;

    private string question;
    private List<string> answerList;
    public GameObject go;
    public Text question_Text;
    public Text[] answer_Text;
    public GameObject[] answerPanel;
    public Animator anim;
    public string keySound;
    public string enterSound;

    public bool choiceing; //대기
    private bool keyInput; //키처리 활성화
    private int count;
    private int result;
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    // Start is called before the first frame update
    void Start()
    {
        Audio = FindObjectOfType<AudioManager>();
        answerList = new List<string>();
        for(int i= 0; i < answer_Text.Length; ++i)
        {
            answer_Text[i].text = "";
            answerPanel[i].SetActive(false);
        }
        question_Text.text = "";
    }

    public void ShowChoice(Choice _choice)
    {
        choiceing = true;
        go.SetActive(true);
        result = 0;
        question = _choice.question;
        choiceing = true;
        for(int i=0; i<_choice.answer.Length; ++i)
        {
            answerList.Add(_choice.answer[i]);
            answerPanel[i].SetActive(true);
            count = i;
        }

        anim.SetBool("Appear", true);
        Selection();
        StartCoroutine(ChoiceCoroutine());
    }

    IEnumerator ChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(TypingQuestion());
        StartCoroutine(TypingAnswer_0());

        if (count > 0)
            StartCoroutine(TypingAnswer_1());
        if (count > 1)
            StartCoroutine(TypingAnswer_2());
        if (count > 2)
            StartCoroutine(TypingAnswer_3());

        yield return new WaitForSeconds(0.5f);

        keyInput = true;
    }

    IEnumerator TypingQuestion()
    {
        for(int i=0; i < question.Length; ++i)
        {
            question_Text.text += question[i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_0()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < answerList[0].Length; ++i)
        {
            answer_Text[0].text += answerList[0][i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_1()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < answerList[1].Length; ++i)
        {
            answer_Text[1].text += answerList[1][i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_2()
    {
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < answerList[2].Length; ++i)
        {
            answer_Text[2].text += answerList[2][i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_3()
    {
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < answerList[3].Length; ++i)
        {
            answer_Text[3].text += answerList[3][i];
            yield return waitTime;
        }
    }

    private void Update()
    {
        if(keyInput)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                Audio.Play(keySound);
                if (result > 0)
                    result--;
                else
                    result = count;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Audio.Play(keySound);
                if (result < count)
                    result++;
                else
                    result = 0;
                Selection();
            }
            else if(Input.GetKeyDown(KeyCode.Z))
            {
                Audio.Play(enterSound);
                keyInput = false;
                ExitChoice();
            }
        }
    }
    public void Selection()
    {
        Color color = answerPanel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for(int i = 0; i <= count; ++i)
        {
            answerPanel[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        answerPanel[result].GetComponent<Image>().color = color;

    }

    public int GetResult()
    {
        return result;
    }

    public void ExitChoice()
    {
        for(int i= 0; i <= count; ++i)
        {
            answer_Text[i].text = "";
            answerPanel[i].SetActive(false);
        }
        answerList.Clear();
        anim.SetBool("Appear", false);
        question_Text.text = "";
        choiceing = false;
        //여기다가 대기를 줘야함
        go.SetActive(false);
    }
}
