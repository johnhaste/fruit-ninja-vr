using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField, Range(0f, 1f)] public float volume = 0.2f;

    [Header("Sound FX")]
    public AudioClip splashSoundEffect;
    public AudioClip canonSoundEffect;

    [Header("Background Music")]
    public AudioClip backgroundMusic1;

    //Singleton
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGMusic(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(backgroundMusic1, position, volume);
    }

    public void PlaySplashSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(splashSoundEffect, position);
    }

    public void PlayCanonSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(canonSoundEffect, position);
    }
}
