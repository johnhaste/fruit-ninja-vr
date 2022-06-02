using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Settings")]
    private AudioSource Audio;
    [SerializeField, Range(0f, 1f)] public float volume = 0.2f;

    [Header("Sound FX")]
    public AudioClip splashSoundEffect;
    public AudioClip canonSoundEffect;
    public AudioClip swordWooshSoundEffect;

    [Header("Background Music")]
    public AudioClip backgroundMusic1;
    public AudioClip backgroundSeaMusic;

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
        Audio =  GetComponents<AudioSource>()[0];
        Audio.clip = backgroundMusic1;
        Audio.loop = true;
        Audio.volume = 0.3f;
        Audio.Play();
    }

    public void PlayBGSeaMusic(Vector3 position)
    {
        Audio = GetComponents<AudioSource>()[1];
        Audio.clip = backgroundSeaMusic;
        Audio.loop = true;
        Audio.volume = 0.2f;
        Audio.Play();
    }

    public void PlaySplashSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(splashSoundEffect, position);
    }

    public void PlayCanonSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(canonSoundEffect, position);
    }

    public void PlaySwordWoosh(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(swordWooshSoundEffect, position);
    }
}
