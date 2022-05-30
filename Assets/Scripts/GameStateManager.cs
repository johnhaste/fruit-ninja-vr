using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState{
        PREGAME, INGAME, ENDGAME, RESTARTINGGAME
    }

    public static GameStateManager instance;
    public GameState currentGameState;

    //Singleton
    private void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
            return;
        }
 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        currentGameState = GameState.INGAME;
    }

    public void EndGame()
    {
        currentGameState = GameState.ENDGAME;
        UIManager.instance.DisplayRestartUI();
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateMessageUI("The end, slash to restart!");
        UIManager.instance.HideUIElement(UIManager.instance.textTime);
    }

    public void RestartGame()
    {
        print("Restarting Game");
        StartCoroutine(WaitAndRestart(2f));
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
