using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup sfxGroup;

    private AudioSource sfxSource;

    void Awake()
    {
        instance = this;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = sfxGroup;
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.pitch = Random.Range(0.92f, 1.08f);

        sfxSource.PlayOneShot(clip, volume);
    }
}