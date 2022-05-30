using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsSpawnerManager : MonoBehaviour
{
    //Fruits
    public GameObject[] Fruitprefabs;
    public GameObject[] FruitsSpawner;
    private int indexFruit;
    private int waveNumber = 1;

    public float timeRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateFruits());
    }

    void Update()
    {
        int secondsLeft = TimeManager.instance.secondsLeft;

        if(secondsLeft > 45){
            timeRate = 5f;
        }else if(secondsLeft > 30){
            timeRate = 4f;
        }else if(secondsLeft > 15){
            timeRate = 3f;
        }else{
            timeRate = 2f;
        }
    }

    private IEnumerator CreateFruits()
    {
        while (true)
        {
            if(GameStateManager.instance.currentGameState == GameStateManager.GameState.INGAME){

                if(waveNumber == 1)
                {
                    indexFruit = Random.Range(0, 4);
                    GameObject fruit = Instantiate(Fruitprefabs[indexFruit], FruitsSpawner[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                    fruit.GetComponent<Rigidbody>().AddForce(3 * new Vector3(2f,5f,0f), ForceMode.Impulse);       
                    fruit.transform.SetParent(transform);
                }
            }
            yield return new WaitForSeconds(timeRate);
            print(timeRate);
        }
    }

}
