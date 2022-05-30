using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState{
        PREGAME, INGAME, ENDGAME
    }

    public static GameStateManager instance;
    public GameState currentGameState;

    //Singleton
    private void Awake(){
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        currentGameState = GameState.INGAME;
    }

    public void EndGame()
    {
        currentGameState = GameState.ENDGAME;
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateMessageUI("The end, slash to restart!");
        UIManager.instance.HideUIElement(UIManager.instance.textTime);
    }
}
