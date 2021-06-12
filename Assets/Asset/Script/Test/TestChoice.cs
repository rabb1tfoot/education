using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChoice : MonoBehaviour
{
    [SerializeField]
    public Choice choice; 

    private OrderManager Order;
    private ChoiceManager ChoiceManager;
    public bool flag;

    void Start()
    {
        Order = FindObjectOfType<OrderManager>();
        ChoiceManager = FindObjectOfType<ChoiceManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!flag)
        {
            StartCoroutine(QuestionCoroutine());
        }

    }

    IEnumerator QuestionCoroutine()
    {
        flag = true;
        Order.NoMove();
        ChoiceManager.ShowChoice(choice);
        yield return new WaitUntil(() => !ChoiceManager.choiceing);

        Order.CanMove();
        Debug.Log(ChoiceManager.GetResult());
    }
}
