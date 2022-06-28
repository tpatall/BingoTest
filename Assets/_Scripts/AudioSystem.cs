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

    public AudioClip buttonClick;

    public void PlayMusic(AudioClip clip) {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    /// <summary>
    ///     Play sound from clip and choose specific volume.
    /// </summary>
    /// <param name="clip">SFX</param>
    /// <param name="reduction">By how much this sound should be reduced in relation to the setting.</param>
    public void PlaySound(AudioClip clip, float reduction) {
        float volume = SoundSource.volume * reduction;
        SoundSource.PlayOneShot(clip, volume);
    }

    /// <summary>
    ///     Play sound from clip with standard settings from soundSource.
    /// </summary>
    /// <param name="clip">SFX</param>
    public void PlaySound(AudioClip clip) {
        SoundSource.PlayOneShot(clip);
    }
}
