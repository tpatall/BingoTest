using UnityEngine;

/// <summary>
///     Basic audio system that persists through scene changes.
/// </summary>
public class AudioSystem : Singleton<AudioSystem>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    public void PlayMusic(AudioClip clip) {
        musicSource.clip = clip;
        musicSource.Play();
    }

    /// <summary>
    ///     Play sound from clip and choose specific volume.
    /// </summary>
    /// <param name="clip">SFX</param>
    /// <param name="vol">Volume</param>
    public void PlaySound(AudioClip clip, float vol) {
        soundSource.PlayOneShot(clip, vol);
    }

    /// <summary>
    ///     Play sound from clip with standard settings from soundSource.
    /// </summary>
    /// <param name="clip">SFX</param>
    public void PlaySound(AudioClip clip) {
        soundSource.PlayOneShot(clip);
    }
}
