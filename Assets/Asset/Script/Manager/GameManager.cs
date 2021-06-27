using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CseneChangeBound[] bounds;
    private PlayerManager Player;
    private CameraManager Caemra;

    public void LoadStart()
    {
        StartCoroutine(LoadWaitCoroutine());
    }

    IEnumerator LoadWaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        Player = FindObjectOfType<PlayerManager>();
        bounds = FindObjectsOfType<CseneChangeBound>();
        Caemra = FindObjectOfType<CameraManager>();

        Caemra.target = GameObject.Find("Player");
        
        for(int i = 0; i < bounds.Length; ++i)
        {
            if(bounds[i].boundName == Player.currentMapName)
            {
                bounds[i].SetBound();
                break;
            }
        }
    }
}
