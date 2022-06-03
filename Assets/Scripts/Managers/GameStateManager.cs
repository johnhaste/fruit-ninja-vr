using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        PREGAME, INGAME, ENDGAME, RESTARTINGGAME
    }

    public static GameStateManager instance;
    public GameState currentGameState;

    //Singleton
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        currentGameState = GameState.INGAME;

        //Update game state
        currentGameState = GameState.INGAME;

        AudioManager.instance.PlayBGMusic(transform.position);
        AudioManager.instance.PlayBGSeaMusic(transform.position);
    }

    public void EndGame()
    {
        currentGameState = GameState.ENDGAME;

        //Sets up a Restart Screen
        //StartCoroutine(WaitAndDisplayRestartUI(2f));
        UIManager.instance.DisplayRestartUI();
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateMessageUI("Gam over!");
        UIManager.instance.HideUIElement(UIManager.instance.textTime);
    }

    public void RestartGame()
    {
        StartCoroutine(WaitAndRestart());
    }

    IEnumerator WaitAndDisplayRestartUI(float duration)
    {
        yield return new WaitForSeconds(duration);
        UIManager.instance.DisplayRestartUI();
    }

    public IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(2f);

        //Update UI
        UIManager.instance.DestroyRestartUI();
        UIManager.instance.HideUIElement(UIManager.instance.textMessage);
        UIManager.instance.DisplayUIElement(UIManager.instance.textTime);

        //Restart Params
        ScoreManager.instance.RestartScore();
        TimeManager.instance.RestartTimer();

        //Stops infinite loop
        StopCoroutine(WaitAndRestart());
        
        //Update game state
        currentGameState = GameState.INGAME;
    }
}
