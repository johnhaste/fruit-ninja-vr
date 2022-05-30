using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int secondsLeft = 65;

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

    private IEnumerator DecreaseTime()
    {
        while (true)
        {
            secondsLeft--;
            if(secondsLeft > 0){
                UIManager.instance.UpdateTimeUI(secondsLeft);
            }
            yield return new WaitForSeconds(1f);
        }
    }


}
