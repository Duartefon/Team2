using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    private Vector2 rotation = Vector2.zero;
    public float SENSITIVITY = 2f;


    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotation.x += Input.GetAxis("Mouse X") * SENSITIVITY;
        rotation.y += Input.GetAxis("Mouse Y") * SENSITIVITY;
        rotation.y = Mathf.Clamp(rotation.y, -80f, 80f);

        transform.localRotation = Quaternion.Euler(-rotation.y, rotation.x, 0);
    }
}
