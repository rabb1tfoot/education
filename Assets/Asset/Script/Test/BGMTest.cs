using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTest : MonoBehaviour
{
    BGMManager BGM;
    public string strmusicName;

    void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BGM.Play(strmusicName);
        this.gameObject.SetActive(false);
    }
}
