using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    public Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckIfMoved());
    }

    private IEnumerator CheckIfMoved()
    {
        while (true)
        {
           float movementDifference = Mathf.Abs(lastPosition.y - transform.position.y);
           print("Difference:"+movementDifference); 
           if(movementDifference > 0.1f){
               AudioManager.instance.PlaySwordWoosh(transform.position);
           }
           lastPosition = transform.position;
           yield return new WaitForSeconds(0.5f);
           //print("Last position:"+lastPosition);
        }
    }
}
