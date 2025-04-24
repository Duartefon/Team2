using UnityEngine;

public class ObjectHP : MonoBehaviour
{
    [SerializeField] private GameObject halfHPObject;
    [SerializeField] private System.String tagForSaveCorrectSpot;

    private float hp = Options.hp;
    private Vector3 startPosition;
    private BodyManager bodyManager;
    private bool isPlaced = false;

    private void Start()
    {
        startPosition = transform.position;
        bodyManager = FindObjectOfType<BodyManager>();
    }

    public void SetHP(float hpValue)
    {
        hp = hpValue;
    }

    public void SetAsPlaced()
    {
        isPlaced = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("unsafe"))
        {
            hp--;

            if (hp == 1)
            {
                Debug.Log("You better watch out!");
                GameObject newObj = Instantiate(halfHPObject, startPosition, Quaternion.identity);
                ObjectHP newHP = newObj.GetComponent<ObjectHP>();
                newHP.halfHPObject = new GameObject();
                newHP.SetHP(hp);
                newHP.bodyManager = bodyManager;
            }
            else
            {
                //Debug.Log("You Fucked Up!");
                if (isPlaced && bodyManager != null)
                {
                    bodyManager.OnOrganPlaced(true); // broken organ placed
                }
            }

            Destroy(gameObject);
        }
        //add colision for the safe tag. aand pass bodyManager(false)
        if (collision.gameObject.CompareTag(tagForSaveCorrectSpot))
        {
            bodyManager.OnOrganPlaced(false);
        }
        if (collision.gameObject.CompareTag("safe"))
        {
            Debug.Log("its safe");
        }


    }
}
