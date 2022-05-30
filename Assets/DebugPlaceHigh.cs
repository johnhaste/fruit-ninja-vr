using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlaceHigh : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            gameObject.transform.position = new Vector3(0f,10f,0f);
        }

    }
}
