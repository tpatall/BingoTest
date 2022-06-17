using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Basic audio system that persists through scene changes.
/// </summary>
public class AudioSystem : PersistentSingleton<AudioSystem>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    public void PlayMusic(AudioClip clip) {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySound(AudioClip clip, float vol) {
        soundSource.PlayOneShot(clip, vol);
    }
}
