using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MusicTrack
{
    public string trackName;
    public AudioClip clip;

    [HideInInspector] public AudioSource source;
}

public class MusicSystem : Singleton<MusicSystem>
{
    [SerializeField] private MusicTrack[] tracks;
    [SerializeField, Range(0f, 1f)] private float volume;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < tracks.Length; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = tracks[i].clip;
            newSource.loop = true;
            newSource.playOnAwake = false;
            newSource.volume = volume;

            tracks[i].source = newSource;
        }
    }

    public void Play(string name)
    {
        MusicTrack track = GetTrack(name);

        if (track == null) return;

        track.source.volume = volume;

        if (track.source.time > 0f && !track.source.isPlaying)
            track.source.UnPause();
        else
            track.source.Play();
    }

    public void Pause(string name)
    {
        MusicTrack track = GetTrack(name);

        if (track == null) return;

        track.source.Pause();
    }

    public void Stop(string name)
    {
        MusicTrack track = GetTrack(name);

        if (track == null) return;

        track.source.Stop();
    }

    public void SetVolume (float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);

        for (int i = 0; i < tracks.Length; i++)
        {
            if (tracks[i].source != null)
                tracks[i].source.volume = volume;
        }
    }

    public void FadeIn(string name, float fadeTime)
    {
        MusicTrack track = GetTrack(name);

        if (track == null) return;

        StartCoroutine(FadeInCoroutine(track, fadeTime));
    }

    public void FadeOut(string name, float fadeTime)
    {
        MusicTrack track = GetTrack(name);

        if (track == null) return;

        StartCoroutine(FadeOutCoroutine(track, fadeTime));
    }

    public void SwitchTo(string name, float fadeTime)
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            if (tracks[i].trackName == name)
                StartCoroutine(FadeInCoroutine(tracks[i], fadeTime));
            else
                StartCoroutine(FadeOutCoroutine(tracks[i], fadeTime));
        }
    }

    private MusicTrack GetTrack(string name)
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            if (tracks[i].trackName == name)
                return tracks[i];
        }

        Debug.LogWarning("Track not found: " + name);
        return null;
    }

    private IEnumerator FadeInCoroutine(MusicTrack track, float fadeTime)
    {
        if (!track.source.isPlaying)
        {
            track.source.volume = 0f;
            track.source.Play();
        }

        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            track.source.volume = Mathf.Lerp(0f, volume, timer / fadeTime);
            yield return null;
        }

        track.source.volume = 1f;
    }

    private IEnumerator FadeOutCoroutine(MusicTrack track, float fadeTime)
    {
        float startVolume = track.source.volume;
        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            track.source.volume = Mathf.Lerp(startVolume, 0f, timer / fadeTime);
            yield return null;
        }

        track.source.volume = 0f;
        track.source.Stop();
        track.source.volume = volume;
    }
}
