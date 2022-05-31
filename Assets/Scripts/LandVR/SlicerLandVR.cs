using UnityEngine;
using EzySlice;
public class SlicerLandVR : MonoBehaviour
{
    public Material defaultMaterial;
    public LayerMask sliceMask;
    public bool isTouched;

    private void Update()
    {
        if (isTouched == true)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);
            
            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                Material insideMaterial;

                //Restart Fruit
                if(objectToBeSliced.gameObject.name == "RestartFruit")
                {
                    GameStateManager.instance.RestartGame();
                    insideMaterial = defaultMaterial;
                }else{
                    insideMaterial = objectToBeSliced.gameObject.GetComponent<Fruit>().insideMaterial;
                }

                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, insideMaterial);

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, insideMaterial);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, insideMaterial);

                upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);

                Destroy(objectToBeSliced.gameObject);

                //Slice Sound
                //AudioManager.instance.sliceSound.gameObject.transform.position = objectToBeSliced.transform.position;
                //AudioManager.instance.sliceSound.Play();

                //Vibration
                VibrationManager.instance.VibrateController(0.4f, 1, 0.3f, OVRInput.Controller.RTouch);

                //Add Score
                ScoreManager.instance.AddScore(50);

                //Destroys the slices after a few seconds
                upperHullGameobject.gameObject.AddComponent<DestroyAfterSeconds>();
                lowerHullGameobject.gameObject.AddComponent<DestroyAfterSeconds>();
            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }


}
