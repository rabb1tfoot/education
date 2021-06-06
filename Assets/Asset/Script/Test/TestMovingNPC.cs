using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestMove
{
    public string name;
    public string direction;
}

public class TestMovingNPC : MonoBehaviour
{
    [SerializeField]
    public TestMove[] move;

    private OrderManager Order;
    void Start()
    {
        Order = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.name =="Player")
        {
            Order.PreLoadCharactor();
            for(int i = 0; i < move.Length; ++i)
            {
                Order.Move(move[i].name, move[i].direction);
            }
        }
    }
}
