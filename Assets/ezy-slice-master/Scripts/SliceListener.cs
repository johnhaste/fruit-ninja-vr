using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListener : MonoBehaviour
{
    public Slicer slicer;
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.layer == LayerMask.NameToLayer("Sliceable"))
        {
          slicer.isTouched = true;
          Debug.Log("Touched a sliceable");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        //slicer.isTouched = false;
    }
}
