using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNumberLock : MonoBehaviour
{
    private OrderManager Order;
    private NumberSystem Number;

    public bool flag;
    public int correctNum;

    void Start()
    {
        Order = FindObjectOfType<OrderManager>();
        Number = FindObjectOfType<NumberSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            StartCoroutine(NumberLockCoroutine());
        }

    }

    IEnumerator NumberLockCoroutine()
    {
        flag = true;
        Order.NoMove();
        Number.ShowNumber(correctNum);
        yield return new WaitUntil(() => !Number.activated);

        Order.CanMove();
        Debug.Log(Number.GetResult());
    }
}
