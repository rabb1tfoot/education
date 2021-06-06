using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeoutTest : MonoBehaviour
{
    BGMManager BGM;

    void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(abd());
    }

    IEnumerator abd()
    {
        BGM.FadeOut();

        yield return new WaitForSeconds(3f);

        BGM.FadeIn();
    }
}
