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
            waveNumber = 1;
        }else if(secondsLeft > 30){
            timeRate = 4f;
            waveNumber = 2;
        }else if(secondsLeft > 15){
            timeRate = 4f;
            waveNumber = 3;
        }else{
            timeRate = 2f;
            //waveNumber = 4;
        }
    }

    private IEnumerator CreateFruits()
    {
        while (true)
        {
            if(GameStateManager.instance.currentGameState == GameStateManager.GameState.INGAME){
                switch(waveNumber){
                    case 1:
                        indexFruitsSpawner = Random.Range(0,FruitsSpawner.Length);
                        StartCoroutine(InstantiateXFruitsSameCanon(1, 1f));
                        break;
                    case 2:
                        indexFruitsSpawner = Random.Range(0,FruitsSpawner.Length);
                        StartCoroutine(InstantiateXFruitsSameCanon(2, 1f));
                        break;
                    case 3:
                        indexFruitsSpawner = Random.Range(0,FruitsSpawner.Length);
                        StartCoroutine(InstantiateXFruitsDifferentCanons(3, 1f));
                        break;
                }
                
            }
            yield return new WaitForSeconds(timeRate);
        }
    }

    IEnumerator InstantiateXFruitsSameCanon(int x, float interval)
    {
        for(int i = 0; i < x; i++){
            yield return new WaitForSeconds(interval);
            InstantiateRandomFruit();
        } 
    }

    IEnumerator InstantiateXFruitsDifferentCanons(int x, float interval)
    {
        for(int i = 0; i < x; i++){
            indexFruitsSpawner = Random.Range(0,FruitsSpawner.Length);
            yield return new WaitForSeconds(interval);
            InstantiateRandomFruit();
        } 
    }

    public void InstantiateRandomFruit()
    {
        if(GameStateManager.instance.currentGameState == GameStateManager.GameState.INGAME)
        {
            indexFruit = Random.Range(0, Fruitprefabs.Length);
            GameObject fruit = Instantiate(Fruitprefabs[indexFruit], FruitsSpawner[indexFruitsSpawner].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            AudioManager.instance.PlayCanonSound(FruitsSpawner[indexFruitsSpawner].transform.position);

            Vector3 canonForce = new Vector3(0f,0f,0f);
            switch(indexFruitsSpawner){
                case 0:
                    //Left
                    canonForce = new Vector3(4f,Random.Range(4,5),Random.Range(-6f,-7f));
                    break;
                case 1:
                    //Center
                    canonForce = new Vector3(0f,Random.Range(4,5),Random.Range(-6f,-7f));
                    break;
                case 2:
                    //Right
                    canonForce = new Vector3(-4f,Random.Range(4,5),Random.Range(-6f,-7f));
                    break;
                case 3:
                    //top-left
                    canonForce = new Vector3(4f,Random.Range(1,2),Random.Range(-10f,-11f));
                    break;
                case 4:
                    //top
                    canonForce = new Vector3(0f,Random.Range(1,2),Random.Range(-10f,-11f));
                    break;
                case 5:
                    //top-right
                    canonForce = new Vector3(-4f,Random.Range(1,2),Random.Range(-10f,-11f));
                    break;
            }
                                    
            fruit.GetComponent<Rigidbody>().AddForce(3 * canonForce, ForceMode.Impulse);       
            fruit.transform.SetParent(transform);
        }
        
    }
}