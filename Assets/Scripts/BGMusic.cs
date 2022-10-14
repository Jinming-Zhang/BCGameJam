using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> tracks;
    private int currentTrackInd;

    private static BGMusic instance;
    public static BGMusic Instance => instance;
    [SerializeField] int fadeTime;

    [SerializeField]
    AudioClip startingclip;
    [SerializeField]
    AudioSource sfxTrack;
    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        currentTrackInd = 0;
    }
    private void Start()
    {
        PlayMusic(startingclip);
    }
    public void Restart()
    {
        PlayMusic(startingclip);
    }

    public void PlayMusic(AudioClip clip)
    {
        FadeOut(tracks[currentTrackInd]);
        currentTrackInd = currentTrackInd + 1 >= tracks.Count ? 0 : currentTrackInd + 1;
        tracks[currentTrackInd].clip = clip;
        tracks[currentTrackInd].loop = true;
        tracks[currentTrackInd].Play();
        FadeIn(tracks[currentTrackInd]);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxTrack.clip = clip;
        sfxTrack.loop = false;
        sfxTrack.Play();
    }
    void FadeOut(AudioSource track)
    {
        StartCoroutine(FadeOutCR());
        IEnumerator FadeOutCR()
        {
            float amt = track.volume;
            while (track.volume > 0)
            {
                float delta = amt / fadeTime * Time.deltaTime;
                track.volume -= delta;
                yield return new WaitForEndOfFrame();
            }
            track.Stop();
        }
    }
    void FadeIn(AudioSource track)
    {
        StartCoroutine(FadeInCR());
        IEnumerator FadeInCR()
        {
            float amt = 1f;
            track.volume = 0;
            while (track.volume < 1)
            {
                float delta = amt / fadeTime * Time.deltaTime;
                track.volume += delta;
                yield return new WaitForEndOfFrame();
            }
        }

    }

    public void StopMusic()
    {
        tracks[currentTrackInd].Stop();
    }
}
