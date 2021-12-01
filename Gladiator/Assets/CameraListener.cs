using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraListener : MonoBehaviour
{
    public float Sensitivity;

    float X,Y;


    void Update() {
        X += Sensitivity * Input.GetAxis("Mouse X");
        Y -= Sensitivity * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(Y,X,0);     
     }
}
