using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public static Menu instance;

    public GameObject obj;
    public AudioManager Audio;

    public string callSound;
    public string cancelSound;

    public OrderManager Order;

    private bool activated;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        activated = false;
        obj.SetActive(false);
        Audio.Play(cancelSound);
        Order.CanMove();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            activated = !activated;

            if(activated)
            {
                Order.NoMove();
                obj.SetActive(true);
                Audio.Play(callSound);
            }
            else
            {
                Order.CanMove();
                obj.SetActive(false);
                Audio.Play(cancelSound);
            }

        }

    }
}
