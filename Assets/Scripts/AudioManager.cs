using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager _instance = null;

    private AudioSource backgroundMusic;
    public AudioClip muisc;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public static AudioManager getInstance()
    {        
        return _instance;
    }

    AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol, float pitch)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        newAudio.pitch = pitch;
        return newAudio;
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance.backgroundMusic = AddAudio(_instance.muisc, true, true, 1, 1);
    }


    public void changeMusic(AudioClip clip)
    {
        if (_instance.backgroundMusic.clip == null || !_instance.backgroundMusic.clip.Equals(clip))
        {
            _instance.backgroundMusic.Stop();
            _instance.backgroundMusic.clip = clip;
            _instance.backgroundMusic.Play();
        }
        if (!_instance.backgroundMusic.isPlaying)
        {
            _instance.backgroundMusic.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


