using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDisabler : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
