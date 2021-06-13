using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputField : MonoBehaviour
{
    public Text text;
    private OrderManager Order;

    // Start is called before the first frame update
    void Start()
    {
        Order = FindObjectOfType<OrderManager>();
        Order.NoMove();
    }

    // Update is called once per frame
    void Update()
    {

     if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(text.text);
            Order.CanMove();
            Destroy(this.gameObject);
        }
    }
}
