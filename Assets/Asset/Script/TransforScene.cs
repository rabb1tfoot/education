using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransforScene : MonoBehaviour
{
    public string transferSceneName;

    private MovingObject Player;

    void Start()
    {
        Player = FindObjectOfType<MovingObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Player.currentMapName = transferSceneName;
            SceneManager.LoadScene(transferSceneName);
        }
    }
}
