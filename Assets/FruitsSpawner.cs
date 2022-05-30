using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsSpawner : MonoBehaviour
{
    public GameObject[] Fruitprefabs;
    private int index;
    private int indexFruit;

    public float timeRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateFruits());
    }

    private IEnumerator CreateFruits()
    {
        while (true)
        {
            indexFruit = Random.Range(0, 4);
            GameObject fruit = Instantiate(Fruitprefabs[indexFruit], this.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            //fruit.transform.localScale = new Vector3(10f,10f,10f);
            //fruit.transform.SetParent(transform);
            yield return new WaitForSeconds(timeRate);
        }
    }

    void OnDrawGizmos(){
	
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(0.2f,0.2f,0.2f));
            
    }

}
