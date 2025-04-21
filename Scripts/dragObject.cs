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

    //Quando se carrega no mouse, desativa-se a gravidade do rigidBody
    void OnMouseDown()
    {
        rb.useGravity = false;
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
            //afeta a velocidade do rigidBody forçando-o a ir na direção do mouse
            rb.velocity = -mOffset * Options.dragSpeed;
        }
        //Força usada para saber se o objeto está neutro, a ser pull ou pushed (W e S).
        //Options tem os parametros para se for necessaro alterar é alterado para todos.
        pushForce = Input.GetAxis("Vertical") * Options.dragSpeed * Options.moveSpeedPercentage;

        //vai buscar a posição do mouse
        Vector3 mousePoint = Input.mousePosition;
        
        //coloca o Z da camera igual ao Z do objeto.
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        //mexe o Z do mouse para o de objeto + a pushforce para o mexer em Z, (Z relativo À visão da camera e não o do world)
        mousePoint.z = mZCoord + pushForce;

        //guarda a informação num Vector3
        cameraToworld = Camera.main.ScreenToWorldPoint(mousePoint);


        //limita o Z maximo e minimo (push/pull) do objeto em relação à camera.
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
        //Guarda a distancia do objeto até À camera
        mOffset = gameObject.transform.position - cameraToworld;
    }
}