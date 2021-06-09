using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public SpriteRenderer white;
    public SpriteRenderer black;

    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public void FadeOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCorroutine(_speed));
    }

    IEnumerator FadeOutCorroutine(float _speed)
    {
        color = black.color;

        while(color.a < 1f)
        {
            color.a += _speed;
            black.color = color;

            yield return waitTime;
        }


    }

    public void FadeIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCorroutine(_speed));
    }

    IEnumerator FadeInCorroutine(float _speed)
    {
        color = black.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;

            yield return waitTime;
        }


    }

    public void Flash(float _speed = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCouroutine(_speed));
    }

    IEnumerator FlashCouroutine(float _speed)
    {
        color = white.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            white.color = color;

            yield return waitTime;
        }
        while (color.a > 0f)
        {
            color.a -= _speed;
            white.color = color;

            yield return waitTime;
        }
    }
    public void FlashOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashOutCorroutine(_speed));
    }

    IEnumerator FlashOutCorroutine(float _speed)
    {
        color = white.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            white.color = color;

            yield return waitTime;
        }


    }

    public void FlashIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashInCorroutine(_speed));
    }

    IEnumerator FlashInCorroutine(float _speed)
    {
        color = white.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            white.color = color;

            yield return waitTime;
        }


    }
}
