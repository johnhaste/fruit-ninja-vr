using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{

    public float seconds = 2.0f;
    
    void Start()
    {
        Destroy(gameObject,seconds);
    }
  
}
