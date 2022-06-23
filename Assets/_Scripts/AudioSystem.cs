using UnityEngine;

/// <summary>
///     Basic audio system that persists through scene changes.
/// </summary>
public class AudioSystem : Singleton<AudioSystem>
{
    [field: SerializeField] 
    public AudioSource MusicSource { get; private set; }
    [field: SerializeField] 
    public AudioSource SoundSource { get; private set; }

    public void PlayMusic(AudioClip clip) {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    /// <summary>
    ///     Play sound from clip and choose specific volume.
    /// </summary>
    /// <param name="clip">SFX</param>
    /// <param name="vol">Volume</param>
    public void PlaySound(AudioClip clip, float vol) {
        SoundSource.PlayOneShot(clip, vol);
    }

    /// <summary>
    ///     Play sound from clip with standard settings from soundSource.
    /// </summary>
    /// <param name="clip">SFX</param>
    public void PlaySound(AudioClip clip) {
        SoundSource.PlayOneShot(clip);
    }
}
