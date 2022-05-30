using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int secondsLeft = 5;
    private int secondsByMatch = 90;

    //Singleton
    public static TimeManager instance;
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
        StartCoroutine(DecreaseTime());
    }

    public void RestartTimer()
    {
        secondsLeft = secondsByMatch;
    }

    private IEnumerator DecreaseTime()
    {
        while (true)
        {
            //Updates the current time UI or ends game
            if(secondsLeft > 0)
            {
                secondsLeft--;
                UIManager.instance.UpdateTimeUI(secondsLeft);
            }else
            {
                StopCoroutine(DecreaseTime());
                GameStateManager.instance.EndGame();
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
