using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Fruit : MonoBehaviour
{
    public Vector3 lastPosition;
    public Material insideMaterial;
    public GameObject splashParticleFruit;

    void Update()
    {
        StartCoroutine(CheckIfMoved());
    }

    private IEnumerator CheckIfMoved()
    {
        while (true)
        {
           if(lastPosition == transform.position){
               this.gameObject.AddComponent<DestroyAfterSeconds>();
           }
           yield return new WaitForSeconds(5f);
           lastPosition = transform.position;
        }
    }

    public void EmitSplash()
    {
        if(splashParticleFruit != null)
        {
            print("Splash");
            GameObject splashParticle = Instantiate(splashParticleFruit, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            //splashParticle.transform.SetParent(transform);
        }
        
    }

}
