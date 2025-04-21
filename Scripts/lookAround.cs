using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAround : MonoBehaviour
{

    float rotationX = 0f;
    float rotationY = 0f;

    public float sensitivity = 25f;

    // Update is called once per frame
    void Update()
    {
        //agarra o eixo X ( | ) e mexesse ao longo dele  
        rotationY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        //agarra o eixo Y ( - ) e mexesse ao longo dele, -1 é para evitar controlo invertido
        rotationX += Input.GetAxis("Mouse Y") * -1 * sensitivity * Time.deltaTime;
        //aplica a transformação
        transform.localEulerAngles =  new Vector3(rotationX, rotationY, 0f);
    }
}
