using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private FadeManager Fade;
    private AudioManager Audio;
    private PlayerManager Player;
    private GameManager GM;

    public string keySound;

    void Start()
    {
        Fade = FindObjectOfType<FadeManager>();
        Audio = FindObjectOfType<AudioManager>();
        Player = FindObjectOfType<PlayerManager>();
        GM = FindObjectOfType<GameManager>();
    }

    public void StartGame()
    {
        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        Fade.FadeOut();
        Audio.Play(keySound);
        yield return new WaitForSeconds(1f);
        Color color = Player.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        Player.GetComponent<SpriteRenderer>().color = color;
        Player.currentMapName = "AMap";

        GM.LoadStart();
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Audio.Play(keySound);
        Application.Quit();
    }
}
