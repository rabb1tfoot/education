using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    private AudioSource source;

    private float Volum;
    private bool loop;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = 50;
    }
    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop()
    {
        source.loop = true;
    }
    public void SetLoopStop()
    {
        source.loop = false;
    }
}
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    [SerializeField]
    public Sound[] sounds;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public void Play(string _name)
    {
        for (int i = 0; i < sounds.Length; ++i)
        if(_name == sounds[i].name)
        {
             sounds[i].Play();
             return;
        }
    }

    public void Stop(string _name)
    {
        for (int i = 0; i < sounds.Length; ++i)
            if (_name == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
    }

    public void SetLoop(string _name)
    {
        for (int i = 0; i < sounds.Length; ++i)
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoop();
                return;
            }
    }

    public void SetLoopStop(string _name)
    {
        for (int i = 0; i < sounds.Length; ++i)
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoopStop();
                return;
            }
    }

    void Start()
    {
        for(int i = 0; i< sounds.Length; ++i)
        {
            GameObject soundObject = new GameObject("���� ���� �̸� : " + i + " " + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }
}
