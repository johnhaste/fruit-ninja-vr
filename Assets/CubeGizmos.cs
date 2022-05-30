using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGizmos : MonoBehaviour
{
    void OnDrawGizmos(){
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawCube(transform.position, new Vector3(1f,1f,1f));     
    }
}
