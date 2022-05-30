using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore;
    public int highScore;

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
       RestartScore(); 
       highScore = PlayerPrefs.GetInt("HighScore",0);
       UpdateHighScore(highScore);
    }

    public void RestartScore()
    {
        currentScore = 0;
    }

    public void AddScore(int points)
    {
        //Adds the points to the current score
        currentScore += points;
        UpdateScore();
    }

    public void UpdateScore()
    {
        //Updates the UI
        UIManager.instance.UpdateScoreUI(currentScore); 

        //Updates the new high score
        if(currentScore > highScore)
        {
           UpdateHighScore(highScore);
           PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }

    public void UpdateHighScore(int score)
    {
        UIManager.instance.UpdateHighScoreUI(score);
    }
}
