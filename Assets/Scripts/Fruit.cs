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

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("SLASH");
            SlashItself();
        }
    }

    public void SlashItself()
    {
        gameObject.GetComponent<Fruit>().EmitSplash();

        SlicedHull slicedObject = SliceObject(gameObject, insideMaterial);

        GameObject upperHullGameobject = slicedObject.CreateUpperHull(gameObject, insideMaterial);
        GameObject lowerHullGameobject = slicedObject.CreateLowerHull(gameObject, insideMaterial);

        upperHullGameobject.transform.position = transform.position;
        lowerHullGameobject.transform.position = transform.position;

        MakeItPhysical(upperHullGameobject);
        MakeItPhysical(lowerHullGameobject);

        Destroy(gameObject);

        //Slice Sound
        AudioManager.instance.PlaySplashSound(transform.position);

        //Vibration
        VibrationManager.instance.VibrateController(0.4f, 1, 0.3f, OVRInput.Controller.RTouch);

        //Add Score
        ScoreManager.instance.AddScore(50);

        //Destroys the slices after a few seconds
        upperHullGameobject.gameObject.AddComponent<DestroyAfterSeconds>();
        lowerHullGameobject.gameObject.AddComponent<DestroyAfterSeconds>();

        //Makes it explode
        Vector3 randomForce = new Vector3(Random.Range(0,3f),Random.Range(0,3f),Random.Range(0,3f));
        upperHullGameobject.GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.Impulse); 
        lowerHullGameobject.GetComponent<Rigidbody>().AddForce((-randomForce), ForceMode.Impulse); 

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
