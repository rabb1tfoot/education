using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public GameObject instance;

    public AudioClip[] clips;

    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this.gameObject;
        }
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Play(string _playMusicTrack)
    {
        source.volume = 1f;
        for(int i = 0; i < clips.Length; ++i)
        {
            if(clips[i].name == _playMusicTrack)
            {
                source.clip = clips[i];
                source.Play();
            }
        }
    }

    public void Pause()
    {
        source.Pause();
    }
    public void UnPause()
    {
        source.UnPause();
    }

    public void SetVolum(float _v)
    {
        source.volume = _v;
    }

    public void Stop()
    {
        source.Stop();
    }
    IEnumerator FadeOutMusicCoroutine()
    {
        for(float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0.0f; i <= 1f; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }
    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

}
