using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Fruit : MonoBehaviour
{
    public Vector3 lastPosition;
    public Material insideMaterial;

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

}
