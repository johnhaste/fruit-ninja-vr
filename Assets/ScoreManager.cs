using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;

    //Singleton
    public static ScoreManager instance;
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


    void Start()
    {
        score = 0;
    }

    public void AddScore(int score)
    {
        this.score += score;
        UIManager.instance.UpdateScore(this.score);
    }
}
