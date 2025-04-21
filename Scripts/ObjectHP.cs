using System.Collections;
using UnityEngine;

public class ObjectHP : MonoBehaviour
{
    [SerializeField] GameObject halfHPObject;

    private float hp = Options.hp;
    private Vector3 startPosition;
    private ParticleSystem ps;

    //Guarda a possição
    private void Start()
    {
        startPosition = transform.position;
        ps = GetComponent<ParticleSystem>();
    }

    public void SetHP(float hpValue)
    {
        hp = hpValue;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("unsafe"))
        {
            hp--;
            if(hp == 1)
            {
                Debug.Log("You better watch out!");

                GameObject newObj = Instantiate(halfHPObject, startPosition, Quaternion.identity);
                ObjectHP newHP = newObj.GetComponent<ObjectHP>();
                newHP.halfHPObject = new GameObject();
                newHP.SetHP(hp);
            }else
            {
                Debug.Log("You Fucked Up!");
            }
            ps.Play();
            ps.transform.parent = null;
            StartCoroutine(DieWithParticles());
            //Destroy(gameObject);
        }
    }
    IEnumerator DieWithParticles()
    {
        if (ps.isPlaying)
        {
            Debug.Log("Waiting");
            yield return null;
        }
        Debug.Log("Done!");
        Destroy(ps.gameObject, ps.main.duration);
        Destroy(gameObject);
    }
}