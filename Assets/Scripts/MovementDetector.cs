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
           float movementDifferenceY =  Mathf.Abs(lastPosition.y - transform.position.y);
           float movementDifferenceX =  Mathf.Abs(lastPosition.x - transform.position.x);
           float movementDifferenceZ =  Mathf.Abs(lastPosition.z - transform.position.z);
           if(movementDifferenceY + movementDifferenceX + movementDifferenceZ > 0.8f ){
               AudioManager.instance.PlaySwordWoosh(transform.position);
           }
           lastPosition = transform.position;
           yield return new WaitForSeconds(0.4f);
        }
    }
}
