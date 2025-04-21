using UnityEngine;

public class ObjectFullHP : MonoBehaviour
{
    [SerializeField] GameObject halfHPObject;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("unsafe"))
        {
            Instantiate(halfHPObject);
            Destroy(gameObject);
        }
    }
}
