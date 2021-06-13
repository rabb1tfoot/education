using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueText : MonoBehaviour
{
    private OrderManager Order;
    private DialogueManager DM;

    public bool flag;
    public string[] Texts;

    void Start()
    {
        Order = FindObjectOfType<OrderManager>();
        DM = FindObjectOfType<DialogueManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            StartCoroutine(DialogueTextCoroutine());
        }

    }

    IEnumerator DialogueTextCoroutine()
    {
        flag = true;
        Order.NoMove();
        DM.ShowText(Texts);
        yield return new WaitUntil(() => DM.talking);
        Order.CanMove();
    }
}
