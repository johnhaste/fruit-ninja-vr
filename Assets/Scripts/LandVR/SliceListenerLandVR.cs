using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListenerLandVR : MonoBehaviour
{
    public SlicerLandVR slicer;
    private void OnTriggerEnter(Collider other)
    {
        slicer.isTouched = true;

        if(other.name == "RestartFruit")
        {
            GameStateManager.instance.RestartGame();
        }
    }
}
