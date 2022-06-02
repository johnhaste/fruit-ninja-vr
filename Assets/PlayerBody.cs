using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{

    public GameObject rightHand;
    public GameObject leftHand;

    // Update is called once per frame
    void Update()
    {
        //Places body between hands
        float distanceBetweenHands = Mathf.Abs(rightHand.transform.position.x - leftHand.transform.position.x);
        float center = leftHand.transform.position.x + (distanceBetweenHands / 2);
        //transform.position = new Vector3(center,7f,-0.3f);

        //Rotates body according to hands
        //transform.eulerAngles = new Vector3(0f,0f,45f);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.name == "CanonBall")
        {
            UIManager.instance.UpdateMessageDebug("TOMOU");
        }
    }
}
