using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OOCScript : MonoBehaviour
{
    private AudioManager Audio;
    public string keySound;
    public string enterSound;
    public string cancelSound;

    public GameObject usePanel;
    public GameObject cancelPanel;

    public Text useText;
    public Text cancelText;

    public bool activated;
    private bool keyInput;
    private bool result = true;

    // Start is called before the first frame update
    void Start()
    {
        Audio = FindObjectOfType<AudioManager>();
    }

    public void Selected()
    {
        Audio.Play(keySound);
        result = !result;
        if (result)
        {
            usePanel.gameObject.SetActive(false);
            cancelPanel.gameObject.SetActive(true);
        }
        else
        {
            usePanel.gameObject.SetActive(true);
            cancelPanel.gameObject.SetActive(false);
        }
    }

    public void ShowChoice(string _useText, string _cancelText)
    {
        activated = true;
        result = true;
        useText.text = _useText;
        cancelText.text = _cancelText;

        usePanel.gameObject.SetActive(false);
        cancelPanel.gameObject.SetActive(false);

        StartCoroutine(ShowChoiceCoroutine());
    }

    IEnumerator ShowChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        keyInput = true;
    }

    public bool GetResult()
    {
        return result;
    }

    void Update()
    {
        if(keyInput)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                Selected();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Selected();
            }
            else if(Input.GetKeyDown(KeyCode.Z))
            {
                Audio.Play(enterSound);
                keyInput = false;
                activated = false;
            }
            else if(Input.GetKeyDown(KeyCode.X))
            {
                Audio.Play(enterSound);
                keyInput = false;
                activated = false;
                result = false;
            }
        }
    }
}
