﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager _instance = null;

    private AudioSource backgroundMusic;
    public AudioClip menuMuisc;
    public AudioClip levelMuisc;

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
        _instance.backgroundMusic = AddAudio(_instance.menuMuisc, true, true, 1, 1);
    }


    public void changeMusic(AudioClip clip)
    {
        Debug.Log(_instance.backgroundMusic.clip.name);
        Debug.Log(clip.name);
        if (_instance.backgroundMusic.clip == null || _instance.backgroundMusic.clip.name != clip.name)
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

    public void playMenuMusic()
    {
        Debug.Log("SWITCHING TO MENU MUSIC");
        if (_instance.backgroundMusic.clip == null || _instance.backgroundMusic.clip.name != menuMuisc.name)
        {
            Debug.Log("WRONG MUSIC FOUND, SWITCHING");
            _instance.backgroundMusic.Stop();
            _instance.backgroundMusic.clip = menuMuisc;
            _instance.backgroundMusic.Play();
        }
        if (!_instance.backgroundMusic.isPlaying)
        {
            Debug.Log("MUSIC NOT PLAYING, STARTING");
            _instance.backgroundMusic.Play();
        }
        else
        {
            Debug.Log("MUSIC ALREADY PLAYING");
        }
    }

    public void playLevelMusic()
    {
        Debug.Log("SWITCHING TO LEVEL MUSIC");
        if (_instance.backgroundMusic.clip == null || _instance.backgroundMusic.clip.name != levelMuisc.name)
        {
            Debug.Log("WRONG MUSIC FOUND, SWITCHING");
            _instance.backgroundMusic.Stop();
            _instance.backgroundMusic.clip = levelMuisc;
            _instance.backgroundMusic.Play();
        }
        if (!_instance.backgroundMusic.isPlaying)
        {
            Debug.Log("MUSIC NOT PLAYING, STARTING");
            _instance.backgroundMusic.Play();
        }
        else
        {
            Debug.Log("MUSIC ALREADY PLAYING");
        }
    }

    public void setVolume(float volume)
    {
        _instance.backgroundMusic.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


