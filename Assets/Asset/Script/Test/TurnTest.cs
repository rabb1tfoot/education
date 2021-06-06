using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTest : MonoBehaviour
{
    public string direction;
    private OrderManager Order;

    void Start()
    {
        Order = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Order.PreLoadCharactor();

            Order.Turn("npc", direction);
        }
    }
}
