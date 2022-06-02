using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Fruit : MonoBehaviour
{
    public Material insideMaterial;
    public GameObject splashParticleFruit;

    public void EmitSplash()
    {
        if(splashParticleFruit != null)
        {
            GameObject splashParticle = Instantiate(splashParticleFruit, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            //splashParticle.transform.SetParent(transform);
        }
        
    }

}
