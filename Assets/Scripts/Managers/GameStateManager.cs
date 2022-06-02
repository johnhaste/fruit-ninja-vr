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
        UIManager.instance.DestroyRestartUI();
        AudioManager.instance.PlayBGMusic(transform.position);
        AudioManager.instance.PlayBGSeaMusic(transform.position);
    }

    public void EndGame()
    {
        currentGameState = GameState.ENDGAME;

        //Sets up a Restart Screen
        StartCoroutine(WaitAndDisplayRestartUI(2f));
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateMessageUI("The end!");
        UIManager.instance.HideUIElement(UIManager.instance.textTime);
    }

    public void RestartGame()
    {
        StartCoroutine(WaitAndRestart(2f));
    }

    IEnumerator WaitAndDisplayRestartUI(float duration)
    {
        yield return new WaitForSeconds(duration);
        UIManager.instance.DisplayRestartUI();
    }

    public IEnumerator WaitAndStartGame()
    {
        UIManager.instance.UpdateMessageUI("3");
        yield return new WaitForSeconds(1f);
        UIManager.instance.UpdateMessageUI("2");
        yield return new WaitForSeconds(1f);
        UIManager.instance.UpdateMessageUI("1");
        yield return new WaitForSeconds(1f);
        StartGame();
    }

    IEnumerator WaitAndRestart(float duration)
    {
        yield return new WaitForSeconds(duration);

        //Update UI
        UIManager.instance.DestroyRestartUI();
        UIManager.instance.HideUIElement(UIManager.instance.textMessage);
        UIManager.instance.DisplayUIElement(UIManager.instance.textTime);

        //Restart Params
        ScoreManager.instance.RestartScore();
        TimeManager.instance.RestartTimer();
        
        //Update game state
        currentGameState = GameState.INGAME;
    }
}
