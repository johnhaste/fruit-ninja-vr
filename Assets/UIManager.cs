using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI textMessage;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textHighScore;
    public TextMeshProUGUI textTime;

    //Singleton
    public static UIManager instance;
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

    public void HideUIElement(TextMeshProUGUI UIElement)
    {
        UIElement.gameObject.SetActive(false);
    }

    public void UpdateScoreUI(int score)
    {
        if(GameStateManager.instance.currentGameState == GameStateManager.GameState.INGAME){
            textScore.text = "Score:" + score;
        }else if (GameStateManager.instance.currentGameState == GameStateManager.GameState.ENDGAME){
            textScore.text = "Final Score:" + score;
        }
        
    }

    public void UpdateHighScoreUI(int score)
    {
        textHighScore.text = "High Score:" + score;
    }

    public void UpdateTimeUI(int seconds)
    {
        float _minutes = Mathf.Floor(seconds / 60);
        float _seconds = Mathf.RoundToInt(seconds%60);
        
        string minutesDisplay = _minutes.ToString();
        string secondsDisplay = _seconds.ToString();

        minutesDisplay = _minutes < 10 ? "0" + _minutes.ToString() : _minutes.ToString();
        secondsDisplay = _seconds < 10 ? "0" + _seconds.ToString() : _seconds.ToString();
  
        textTime.text = "Time left:" + minutesDisplay + ":" + secondsDisplay;
    }

    public void UpdateMessageUI(string message){
        textMessage.text = message;
    }

}
