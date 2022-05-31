using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsSpawnerManager : MonoBehaviour
{
    //Fruits
    public GameObject[] Fruitprefabs;
    public GameObject[] FruitsSpawner;
    private int indexFruit;
    private int indexFruitsSpawner;
    private int waveNumber = 1;

    public float timeRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateFruits());
    }

    void Update()
    {
        int secondsLeft = TimeManager.instance.GetSecondsLeft();

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
                    indexFruit = Random.Range(0, Fruitprefabs.Length);
                    indexFruitsSpawner = Random.Range(0,FruitsSpawner.Length);
                    GameObject fruit = Instantiate(Fruitprefabs[indexFruit], FruitsSpawner[indexFruitsSpawner].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                    
                    Vector3 canonForce = new Vector3(0f,0f,0f);
                    switch(indexFruitsSpawner){
                        case 0:
                            //Left
                            canonForce = new Vector3(4f,Random.Range(4,6),-6f);
                            break;
                        case 1:
                            //Center
                            canonForce = new Vector3(0f,Random.Range(4,6),-6f);
                            break;
                        case 2:
                            //Right
                            canonForce = new Vector3(-4f,Random.Range(4,6),-5f);
                            break;
                        case 3:
                            //top
                            canonForce = new Vector3(0f,Random.Range(1,3),-11f);
                            break;
                    }
                    
                    
                    fruit.GetComponent<Rigidbody>().AddForce(3 * canonForce, ForceMode.Impulse);       
                    fruit.transform.SetParent(transform);
                }
            }
            yield return new WaitForSeconds(timeRate);
            //print(timeRate);
        }
    }

}
