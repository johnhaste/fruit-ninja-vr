using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    float rotationsPerMinute = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,6.0f*rotationsPerMinute*Time.deltaTime,0);
    }
}
