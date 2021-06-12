using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLight : MonoBehaviour
{
    public GameObject obj;

    private bool flag = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(flag)
        {
            flag = false;
            obj.SetActive(true);
        }
    }
}
