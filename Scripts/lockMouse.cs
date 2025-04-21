using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockMouse : MonoBehaviour
{


    void Start()
    {
        
        //prende o rato no centro da tela
        //Cursor.lockState = CursorLockMode.Locked;
        //remove o cursor
        //Cursor.visible = false;
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown("1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown("2"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
