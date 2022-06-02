using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //Fruits
    public GameObject[] fruitprefabs;
    public GameObject[] spawnerPoint;
    public GameObject[] canons;
    private int indexFruit;
    
    //Waves
    private int waveNumber = 1;
    public float timeRate;

    //Canon
    public GameObject canonBall;
    private Vector3 canonForce = new Vector3(0f,0f,0f);
    
    void Start()
    {
        StartCoroutine(CreateFruits());
    }

    void Update()
    {
        int secondsLeft = TimeManager.instance.GetSecondsLeft();

        if(secondsLeft > 90)
        {
            waveNumber = 1;
            timeRate = 5f;
        }
        else if(secondsLeft > 60)
        {
            waveNumber = 2;
            timeRate = 4f;
        }
        else if(secondsLeft > 40)
        {
            waveNumber = 3;
        }
        else if(secondsLeft > 20)
        {
            waveNumber = 4;
            timeRate = 3f;
        }
        else{
            waveNumber = 5; 
        }
    }

    private IEnumerator CreateFruits()
    {
        while (true)
        {
            if(GameStateManager.instance.currentGameState == GameStateManager.GameState.INGAME)
            {
                int indexCurrentSpawner;
                switch(waveNumber){
                    case 1:
                        indexCurrentSpawner = Random.Range(0,spawnerPoint.Length);
                        StartCoroutine(InstantiateXFruitsSameCanon(indexCurrentSpawner, 1, 1f));
                        break;
                    case 2:
                        indexCurrentSpawner = Random.Range(0,spawnerPoint.Length);
                        StartCoroutine(InstantiateXFruitsSameCanon(indexCurrentSpawner,2, 1f));
                        break;
                    case 3:
                        indexCurrentSpawner = Random.Range(0,spawnerPoint.Length);
                        StartCoroutine(InstantiateXFruitsDifferentCanons(2, 1f));
                        break;
                    case 4:
                        indexCurrentSpawner = Random.Range(0,spawnerPoint.Length);
                        StartCoroutine(InstantiateXFruitsDifferentCanons(3, 1f));
                        break;
                    case 5:
                        indexCurrentSpawner = Random.Range(0,spawnerPoint.Length);
                        StartCoroutine(InstantiateXFruitsDifferentCanons(3, 1f));
                        //Canonball
                        /*if(Random.Range(0,2) == 1)
                        {
                            StartCoroutine(InstantiateXFruitsDifferentCanons(3, 1f));
                        }else{
                            StartCoroutine(InstantiateCanonBall(1f));
                        }*/
                        
                        break;
                }
                
            }
            yield return new WaitForSeconds(timeRate);
        }
    }

    IEnumerator InstantiateXFruitsSameCanon(int indexCurrentSpawner,int fruitsAmount, float shootInterval)
    {
        for(int i = 0; i < fruitsAmount; i++)
        {
            InstantiateItemInSpawnPoint(indexCurrentSpawner, "fruit");
            yield return new WaitForSeconds(shootInterval);
        } 
    }

    IEnumerator InstantiateCanonBall(int indexCurrentSpawner,float shootInterval)
    {
        yield return new WaitForSeconds(shootInterval);
        InstantiateItemInSpawnPoint(Random.Range(0, spawnerPoint.Length),"canonball");
    }

    IEnumerator InstantiateXFruitsDifferentCanons(int x, float shootInterval)
    {
        for(int i = 0; i < x; i++)
        {
            int indexCurrentSpawner = Random.Range(0,spawnerPoint.Length);
            yield return new WaitForSeconds(shootInterval);
            InstantiateItemInSpawnPoint(Random.Range(0, spawnerPoint.Length),"fruit");
        } 
    }

    public void InstantiateItemInSpawnPoint(int indexCurrentSpawner, string typeOfObject)
    {
        if(GameStateManager.instance.currentGameState == GameStateManager.GameState.INGAME)
        {
            GameObject item;

            //Animate Canon
            //canons[indexCurrentSpawner].GetComponent<Animator>().SetTrigger("Shoot");

            if(typeOfObject == "fruit"){
                //Selects a random fruit
                indexFruit = Random.Range(0, fruitprefabs.Length);

                //Instantiates the fruit in a random canon
                item = Instantiate(fruitprefabs[indexFruit], spawnerPoint[indexCurrentSpawner].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            
            }else{
                
                //Instantiates the canonBall in a random canon
                item = Instantiate(canonBall, spawnerPoint[indexCurrentSpawner].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            }
            
            //Play canon sound
            AudioManager.instance.PlayCanonSound(spawnerPoint[indexCurrentSpawner].transform.position);

            switch(indexCurrentSpawner){
                case 0:
                    //Left
                    canons[0].GetComponent<Animator>().SetTrigger("Shoot");
                    canonForce = new Vector3(4f,Random.Range(4,5),Random.Range(-6f,-7f));
                    break;
                case 1:
                    //Center
                    canons[1].GetComponent<Animator>().SetTrigger("Shoot");
                    canonForce = new Vector3(0f,Random.Range(4,5),Random.Range(-6f,-7f));
                    break;
                case 2:
                    //Right
                    canons[2].GetComponent<Animator>().SetTrigger("Shoot");
                    canonForce = new Vector3(-4f,Random.Range(4,5),Random.Range(-6f,-7f));
                    break;
                case 3:
                    //top-left
                    canons[3].GetComponent<Animator>().SetTrigger("Shoot");
                    canonForce = new Vector3(4f,Random.Range(1,2),-11f);
                    break;
                case 4:
                    //top
                    canons[4].GetComponent<Animator>().SetTrigger("Shoot");
                    canonForce = new Vector3(0f,Random.Range(1,2),-11f);
                    break;
                case 5:
                    //top-right
                    canons[5].GetComponent<Animator>().SetTrigger("Shoot");
                    canonForce = new Vector3(-4f,Random.Range(1,2),-11f);
                    break;
            }
                                    
            item.GetComponent<Rigidbody>().AddForce(3 * canonForce, ForceMode.Impulse);       
            item.transform.SetParent(transform);
        }
    }
}