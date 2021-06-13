using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    static public WeatherManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private AudioManager Audio;
    public ParticleSystem Weather;
    public string weatherSound;
    private WaitForSeconds waitTime = new WaitForSeconds(3f);
    float loadtime = 0;
    float Timer = 0;
    void Start()
    {
        Audio = FindObjectOfType<AudioManager>();
    }

    public void StartWeather()
    {
        Weather.Play();
        Audio.SetLoop(weatherSound);
        Audio.Play(weatherSound);
    }

    public void StopWeather()
    {
        StopAllCoroutines();
        Audio.Stop(weatherSound);
        Audio.SetLoopStop(weatherSound);
        Weather.Stop();
        loadtime = 0f;
    }

    public void WeatherDrop()
    {
        StartCoroutine(CouroutineStartDrop());
        Timer += Time.deltaTime;

        if (Timer > 1f)
        {

            Weather.Play();
            Audio.SetLoop(weatherSound);
            Audio.Play(weatherSound);
        }

    }

    IEnumerator CouroutineStartDrop()
    {
        while(loadtime < 1f)
        {
            loadtime += 0.1f;
            Weather.Emit((int)(100 * loadtime));
            yield return waitTime;
        }
        yield break;
    }
}

