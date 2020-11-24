﻿using System.Collections;
using UnityEngine;

public static class SoundManager 
{

    public enum Sound
    {
        Background, 
        Shoot,
        Win,
        Click,
        CantPlace
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

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }
}
