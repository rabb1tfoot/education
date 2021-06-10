using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeather : MonoBehaviour
{
    private WeatherManager Weather;
    public bool rain;
    void Start()
    {
        Weather = FindObjectOfType<WeatherManager>();
    }

    // Update is called once per frame
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (rain)
            Weather.StartWeather();
        else
            Weather.StopWeather();
    }
}
