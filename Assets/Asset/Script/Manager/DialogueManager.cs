using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
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

    public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererDialogueWindow;

    private List<string> listSentences;
    private List<Sprite> listSprites;
    private List<Sprite> listDialougeWindows;

    private int count; // 대화진행상황 카운트

    public Animator animSprite;
    public Animator animDialogueWindow;

    public string typeSound;
    public string enterSound;

    private AudioManager Audio;
    private OrderManager Order;
    public bool talking = false;
    private bool keyActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        count = 0;
        listSentences = new List<string>();
        listSprites = new List<Sprite>();
        listDialougeWindows = new List<Sprite>();
        Audio = FindObjectOfType<AudioManager>();
        Order = FindObjectOfType<OrderManager>();
    }

    public void ShowDialogue(Dialogue _dialogue)
    {
        if(talking)
        {
            return;
        }
        talking = true;

        Order.NoMove();

        for (int i = 0; i < _dialogue.sentences.Length; ++i)
        {
            listSentences.Add(_dialogue.sentences[i]);
            listSprites.Add(_dialogue.sprites[i]);
            listDialougeWindows.Add(_dialogue.dialogueWindows[i]);
        }
        animSprite.SetBool("Appear", true);
        animDialogueWindow.SetBool("Appear", true);
        StartCoroutine(StartDialogueCoroutine());

    }

    IEnumerator StartDialogueCoroutine()
    {

        if (count > 0)
        {
            if (listDialougeWindows[count] != listDialougeWindows[count - 1])
            {
                animSprite.SetBool("Change", true);
                animDialogueWindow.SetBool("Appear", false);
                yield return new WaitForSeconds(0.2f);
                rendererDialogueWindow.sprite = listDialougeWindows[count];
                rendererSprite.sprite = listSprites[count];
                animDialogueWindow.SetBool("Appear", true);
                animSprite.SetBool("Change", false);
            }
            else
            {

                if (listSprites[count] != listSprites[count - 1])
                {
                    animSprite.SetBool("Change", true);
                    yield return new WaitForSeconds(0.01f);
                    rendererSprite.sprite = listSprites[count];
                    animSprite.SetBool("Change", false);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        else
        {
            rendererDialogueWindow.sprite = listDialougeWindows[count];
            rendererSprite.sprite = listSprites[count];
        }
        keyActivated = true;
        for (int i = 0; i < listSentences[count].Length; ++i)
        {
            text.text += listSentences[count][i]; // count번째 문장의  i번째 문자 추가
            if(i % 7 == 1)
            {
                Audio.Play(typeSound);
            }
            yield return new WaitForSeconds(0.01f);
        }

    }
    public void ExitDialogue()
    {

        animSprite.SetBool("Appear", false);
        animDialogueWindow.SetBool("Appear", false);
        count = 0;
        text.text = "";
        listSentences.Clear();
        listSprites.Clear();
        listDialougeWindows.Clear();
        talking = false;
        Order.CanMove();
    }


    // Update is called once per frame
    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                keyActivated = false;
                ++count;
                text.text = "";
                Audio.Play(enterSound);
                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                    animSprite.SetBool("Appear", false);
                    animDialogueWindow.SetBool("Appear", false);
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDialogueCoroutine());
                }
            }
        }
    }
}
