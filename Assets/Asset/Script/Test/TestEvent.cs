using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager DM;
    private OrderManager Order;
    private PlayerManager Player;
    private FadeManager Fade;

    bool isActive = true;

    void Start()
    {
        DM = FindObjectOfType<DialogueManager>();
        Order = FindObjectOfType<OrderManager>();
        Player = FindObjectOfType<PlayerManager>();
        Fade = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isActive && Input.GetKey(KeyCode.Z) && Player.animator.GetFloat("DirY") == 1f)
        {
            isActive = false;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        Order.PreLoadCharactor();

        Order.NoMove();

        DM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !DM.talking);
        Order.Move("Player", "RIGHT");
        Order.Move("Player", "RIGHT");
        Order.Move("Player", "UP");
        yield return new WaitUntil(() => Player.queue.Count == 0);

        Fade.Flash();

        DM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !DM.talking);

        Order.CanMove();
    }
}
