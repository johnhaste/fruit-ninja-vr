using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Fruit : MonoBehaviour
{
    public GameObject slicer;
    public Vector3 lastPosition;
    public Material MaterialAfterSlice;
    public LayerMask sliceMask;
    public Material red;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Blade")
        {
            SlicedHull slicedObject = SliceObject(this.gameObject, MaterialAfterSlice);

            if(slicedObject != null){
                GameObject upperHullGameobject = slicedObject.CreateUpperHull(this.gameObject, MaterialAfterSlice);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(this.gameObject, MaterialAfterSlice);

                upperHullGameobject.transform.position = this.transform.position;
                lowerHullGameobject.transform.position = this.transform.position;
                
                MakeItPhysical(upperHullGameobject, this.gameObject.GetComponent<Rigidbody>().velocity);
                MakeItPhysical(lowerHullGameobject, this.gameObject.GetComponent<Rigidbody>().velocity);

                Destroy(this.gameObject);
            }else{
                print("CORTE NAO FOI");
                //this.gameObject.GetComponent<Renderer>().material = red;
            }

            //Slice Sound
            //AudioManager.instance.sliceSound.gameObject.transform.position = objectToBeSliced.transform.position;
            //AudioManager.instance.sliceSound.Play();

            //Restart Fruit
            if(this.name == "RestartFruit")
            {
                GameStateManager.instance.RestartGame();
            }

            //Vibration
            VibrationManager.instance.VibrateController(0.4f, 1, 0.3f, OVRInput.Controller.RTouch);

            //Add Score
            ScoreManager.instance.AddScore(50);
        }
    }

    private void MakeItPhysical(GameObject obj, Vector3 _velocity)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().velocity = -_velocity;

        int randomNumberX = Random.Range(0,2);
        int randomNumberY = Random.Range(0, 2);
        int randomNumberZ = Random.Range(0, 2);

        obj.GetComponent<Rigidbody>().AddForce(3*new Vector3(randomNumberX,randomNumberY,randomNumberZ),ForceMode.Impulse);       
        obj.AddComponent<DestroyAfterSeconds>();

    }

   
    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        return this.gameObject.Slice(slicer.transform.position, slicer.transform.up, crossSectionMaterial);
    }
}
