using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{ 
    private AudioSource bgMusic;
    [SerializeField] int fadeTime;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        bgMusic = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (bgMusic.isPlaying) return;
        bgMusic.Play();
    }

    public void StopMusic()
    {
        bgMusic.Stop();
    }
    public void FadeMusic()
    {
        if (fadeTime == 0)
        {
            bgMusic.volume = 0;
            
        }
        float t = fadeTime;
        while (t > 0)
        {
            t -= Time.deltaTime;
            bgMusic.volume = t / fadeTime;
        }
    }

}
