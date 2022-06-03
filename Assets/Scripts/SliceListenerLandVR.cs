using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListenerLandVR : MonoBehaviour
{
    public SlicerLandVR slicer;
    public Material redMaterial;

    private void OnTriggerEnter(Collider other)
    {
        slicer.isTouched = true;

        if(other.name == "RestartFruit"){
            other.GetComponent<Fruit>().SlashItself();
            StartCoroutine(GameStateManager.instance.WaitAndRestart());
        }
    }

}
