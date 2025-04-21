using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHalfHP : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("unsafe"))
        { 
            Destroy(gameObject);
        }
    }
}
