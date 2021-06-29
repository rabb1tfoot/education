using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CseneChangeBound[] bounds;
    private PlayerManager Player;
    private CameraManager CameraM;
    private FadeManager Fade;
    private Menu Menu;
    private DialogueManager DM;
    private Camera Cam;
    public void LoadStart()
    {
        StartCoroutine(LoadWaitCoroutine());
    }

    IEnumerator LoadWaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        Player = FindObjectOfType<PlayerManager>();
        bounds = FindObjectsOfType<CseneChangeBound>();
        CameraM = FindObjectOfType<CameraManager>();
        Fade = FindObjectOfType<FadeManager>();
        Menu = FindObjectOfType<Menu>();
        DM = FindObjectOfType<DialogueManager>();
        Cam = FindObjectOfType<Camera>();

        Color color = Player.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        Player.GetComponent<SpriteRenderer>().color = color;

        CameraM.target = GameObject.Find("Player");
        Menu.GetComponent<Canvas>().worldCamera = Cam;
        DM.GetComponent<Canvas>().worldCamera = Cam;

        for (int i = 0; i < bounds.Length; ++i)
        {
            if(bounds[i].boundName == Player.currentMapName)
            {
                bounds[i].SetBound();
                break;
            }
        }
        Fade.FadeIn();
    }
}
