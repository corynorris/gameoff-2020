﻿using System.Collections;
using UnityEngine;

public static class SoundManager
{

    public enum Sound
    {
        Background, 
        Shoot,
        Win,
        Lose,
        Click,
        CantPlace,
        PlayButton,
        PositiveClick,
        NegativeClick,
        SlimeSelect
    }


    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipsArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }    
        }
        Debug.LogError("Sound " + sound + "not found!");
        return null;
    }

    public static void PlaySound(Sound sound, float volume = 1, float pitch = 1)
    {
        PlaySound(sound, Time.timeScale, volume, pitch);
    }


    public static void PlaySound(Sound sound,float speed,float volume = 1, float pitch = 1)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(GetAudioClip(sound));
        GameObject.Destroy(soundGameObject, GetAudioClip(sound).length* speed);
    }
}
