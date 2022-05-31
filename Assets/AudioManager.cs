using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip splashSoundEffect;

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

    public void PlaySplashSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(splashSoundEffect, position);
    }
}
