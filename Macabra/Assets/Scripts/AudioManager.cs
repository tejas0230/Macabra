using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    bool ifPlayed = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex==0)
        {
            Play("Music");
        }
        else if(SceneManager.GetActiveScene().buildIndex==1)
        {
            Stop("Music");
            Play("Ambience"); 
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            Stop("Music");
            Stop("Ambience");
            Play("Hum");
        }
    }

    private void Update()
    {
        /*if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(!ifPlayed)
            {
                Play("Music");
                ifPlayed = true;
            }
                
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(ifPlayed)
            {
                Stop("Music");
                Play("Ambience");
                ifPlayed = false;
            }
            
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (!ifPlayed)
            {
                Stop("Music");
                Stop("Ambience");
                ifPlayed = true;
            }
        }*/
    }
    public void Play(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        {
            if(s==null)
            {
                return;
            }
            s.source.Stop();
        }
    }
}
