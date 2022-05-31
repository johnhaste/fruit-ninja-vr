using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListener : MonoBehaviour
{
    public Slicer slicer;
    public GameObject slicerGameObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sliceable"))
        {
          //slicerGameObject.SetActive(true);
          slicer.isTouched = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sliceable"))
        {
            //slicerGameObject.SetActive(false);
        }
    }
}
