using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dragObject : MonoBehaviour
{
    
    private Vector3 mOffset;
    private float mZCoord;
    private float pushForce = 0;
    private Rigidbody rb;
    private Vector3 cameraToworld;
    public Material outlineMaterial;

    //Quando se carrega no mouse, desativa-se a gravidade do rigidBody
    void OnMouseDown()
    {
        rb.useGravity = false;
        outlineMaterial.SetColor("Color", Color.yellow);
        this.gameObject.GetComponent<Renderer>().materials[0] = outlineMaterial;
    }

    //Quando se levanta o mouse ativase-se a gravidade do rigidBody
    void OnMouseUp()
    {
        rb.useGravity = true;
    }
    //vai buscar o rigidBody, e guarda numa variavel.
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //se tiver gravidade mete a pushForce a 0 e sai do update
        if (rb.useGravity) {
            pushForce = 0;
            return; 
        }
        else
        {
            //afeta a velocidade do rigidBody for�ando-o a ir na dire��o do mouse
            rb.linearVelocity = -mOffset * Options.dragSpeed;
        }
        //For�a usada para saber se o objeto est� neutro, a ser pull ou pushed (W e S).
        //Options tem os parametros para se for necessaro alterar � alterado para todos.
        pushForce = Input.GetAxis("Vertical") * Options.dragSpeed * Options.moveSpeedPercentage;

        //vai buscar a posi��o do mouse
        Vector3 mousePoint = Input.mousePosition;
        
        //coloca o Z da camera igual ao Z do objeto.
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        //mexe o Z do mouse para o de objeto + a pushforce para o mexer em Z, (Z relativo � vis�o da camera e n�o o do world)
        mousePoint.z = mZCoord + pushForce;

        //guarda a informa��o num Vector3
        cameraToworld = Camera.main.ScreenToWorldPoint(mousePoint);


        //limita o Z maximo e minimo (push/pull) do objeto em rela��o � camera.
        if (cameraToworld.z < Options.minDist)
        {
            pushForce = 0;
            cameraToworld.z = Options.minDist;
        }
        if(cameraToworld.z > Options.maxDist)
        {
            pushForce = 0;
            cameraToworld.z = Options.maxDist;
        }
        //Guarda a distancia do objeto at� � camera
        mOffset = gameObject.transform.position - cameraToworld;
    }
}